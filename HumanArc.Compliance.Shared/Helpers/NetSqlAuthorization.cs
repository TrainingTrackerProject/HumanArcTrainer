using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Linq;
using System.Management;
using System.Net.NetworkInformation;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Windows.Forms.VisualStyles;
using HumanArc.Foundation.Data.DataContracts;
using NetSqlAzMan;
using NetSqlAzMan.Cache;
using NetSqlAzMan.ENS;
using NetSqlAzMan.Interfaces;
using NetSqlAzMan.LINQ;

namespace HumanArc.Foundation.Security
{
    /// <summary>
    /// Provides authorization functions using the NetSqlAzMan library.
    /// </summary>
    public class NetSqlAuthorization
    {
        private readonly IAzManStorage _azManStorage;
        private readonly string _applicationName;
        private readonly string _storeName;

        public NetSqlAuthorization(string connectionString, string storeName, string applicationName)
        {
            _storeName = storeName;
            _applicationName = applicationName;
            _azManStorage = new SqlAzManStorage(connectionString);
            
        }

        #region Public Methods

        public bool Authorize(string userName, string itemName)
        {
            AuthorizationType access = AuthorizationType.Deny;
            var dbIdendity = GetIdentityFromUserDatabase(userName);
            if (dbIdendity != null)
            {
                access = _azManStorage.CheckAccess(_storeName, _applicationName, itemName, dbIdendity, DateTime.Now, false);
            }
            else
            {
                var identity = GetIdentityFromUserName(userName);
                access = _azManStorage.CheckAccess(_storeName, _applicationName, itemName, identity, DateTime.Now, false);
            }
            return access == AuthorizationType.Allow || access == AuthorizationType.AllowWithDelegation;
        }



        public List<string> GetOperationsForUser(string userName)
        {
            var dbIdendity = GetIdentityFromUserDatabase(userName);
            if (dbIdendity != null)
            {
                var application = _azManStorage[_storeName][_applicationName];
                var ops = (from item in application.GetItems(ItemType.Operation)
                           let access = item.CheckAccess(dbIdendity, DateTime.Now)
                           where (access == AuthorizationType.Allow || access == AuthorizationType.AllowWithDelegation)
                           select item.Name).ToList();
                return ops;

            }
            else
            {
                var identity = GetIdentityFromUserName(userName);
                var application = _azManStorage[_storeName][_applicationName];
                var ops = (from item in application.GetItems(ItemType.Operation)
                           let access = item.CheckAccess(identity, DateTime.Now)
                           where (access == AuthorizationType.Allow || access == AuthorizationType.AllowWithDelegation)
                           select item.Name).ToList();
                return ops;
            }

        }

        public List<string> GetRolesForUser(string userName)
        {
            var dbIdendity = GetIdentityFromUserDatabase(userName);
            if (dbIdendity != null)
            {
                var application = _azManStorage[_storeName][_applicationName];
                var ops = (from item in application.GetItems(ItemType.Role)
                           let access = item.CheckAccess(dbIdendity, DateTime.Now)
                           where (access == AuthorizationType.Allow || access == AuthorizationType.AllowWithDelegation)
                           select item.Name).ToList();
                return ops;
            }
            else
            {
                var identity = GetIdentityFromUserName(userName);
                var application = _azManStorage[_storeName][_applicationName];

                var ops = (from item in application.GetItems(ItemType.Role)
                           let access = item.CheckAccess(identity, DateTime.Now)
                           where (access == AuthorizationType.Allow || access == AuthorizationType.AllowWithDelegation)
                           select item.Name).ToList();
                return ops;
            }

        }

        public List<string> GetAllRoles()
        {
            var application = _azManStorage[_storeName][_applicationName];
            return application.GetItems(ItemType.Role).Select(item => item.Name).ToList();
        }

        public Dictionary<string,List<AzmanAuthorizationDataContract>> GetRolesAndUsers()
        {
            Dictionary<string, List<AzmanAuthorizationDataContract>> roleUserList = new Dictionary<string, List<AzmanAuthorizationDataContract>>();
            var application = _azManStorage[_storeName][_applicationName];
            var roles = application.GetItems(ItemType.Role);
            foreach (var role in roles)
            {
                var auths = role.GetAuthorizations();
                var userList = new List<AzmanAuthorizationDataContract>();
                foreach (var auth in auths)
                {
                    var entry = new DirectoryEntry(string.Format("LDAP://<SID={0}>", auth.SID.StringValue));
                    userList.Add(
                        new AzmanAuthorizationDataContract() {
                            DisplayName = entry.Properties["displayName"].Value.ToString(),
                            UserName = entry.Properties["sAMAccountName"].Value.ToString(),
                            SID = entry.NativeGuid
                        });
                }
                roleUserList[role.Name] = userList;
            }
            return roleUserList;
        } 

        public List<AzManItemDataContract> GetOperationsInRole(string roleName)
        {
            var application = _azManStorage[_storeName][_applicationName];
            var item = application.GetItem(roleName);
            //return item.Members.Select(t => t.Key).ToList();

            List<string> operationList = item.Members.Select(x => x.Key).ToList();
            
            List<AzManItemDataContract> opsList = new List<AzManItemDataContract>();
            
            foreach (var operation in operationList)
            {
                AzManItemDataContract azMan = new AzManItemDataContract();
                azMan.Id = item.Members[operation].ItemId;
                azMan.Name = operation;
                azMan.Type = TypeOfAzManItem.Operation;
                azMan.Description = item.Members[operation].Description;
                opsList.Add(azMan);
            }

            return opsList;
        }

        public void AddUserToRole(string roleName, string userName)
        {
            ExecuteStorageTransation(
                () =>
                {

                    var dbIdendity = GetIdentityFromUserDatabase(userName);
                    if (dbIdendity != null)
                    {

                        var application = _azManStorage[_storeName][_applicationName];

                        var role = application.GetItems(ItemType.Role).FirstOrDefault(t => t.Name == roleName);
                        if (role == null)
                        {
                            throw new ArgumentException("Given role does not exist in store: " + roleName);
                        }

                        IAzManSid sid = new SqlAzManSID(dbIdendity.CustomSid.StringValue, true);

                        role.CreateAuthorization(
                            sid,
                            WhereDefined.Database,
                            sid,
                            WhereDefined.Database,
                            AuthorizationType.AllowWithDelegation,
                            null,
                            null);
                    }
                    else
                    {
                        var identity = GetIdentityFromUserName(userName);
                        var application = _azManStorage[_storeName][_applicationName];

                        var role = application.GetItems(ItemType.Role).FirstOrDefault(t => t.Name == roleName);
                        if (role == null)
                        {
                            throw new ArgumentException("Given role does not exist in store: " + roleName);
                        }

                        IAzManSid sid = new SqlAzManSID(identity.User);

                        role.CreateAuthorization(
                            sid,
                            WhereDefined.LDAP,
                            sid,
                            WhereDefined.LDAP,
                            AuthorizationType.AllowWithDelegation,
                            null,
                            null);
                    }

                });
        }

        public void RemoveUserFromRole(string roleName, string userName)
        {
            ExecuteStorageTransation(
                () =>
                {

                    var dbIdendity = GetIdentityFromUserDatabase(userName);
                    if (dbIdendity != null)
                    {
                        var application = _azManStorage[_storeName][_applicationName];

                        var role = application.GetItems(ItemType.Role).FirstOrDefault(t => t.Name == roleName);
                        if (role == null)
                        {
                            throw new ArgumentException("Given role does not exist in store: " + roleName);
                        }

                        var roleAuth = role.Authorizations.FirstOrDefault(t => t.SID.StringValue == dbIdendity.CustomSid.StringValue);
                        if (roleAuth == null)
                        {
                            throw new ArgumentException(String.Format("Authorization not found for user {0} in role {1}", userName, roleName));
                        }

                        roleAuth.Delete();
                    }
                    else
                    {
                        var identity = GetIdentityFromUserName(userName);
                        var application = _azManStorage[_storeName][_applicationName];

                        var role = application.GetItems(ItemType.Role).FirstOrDefault(t => t.Name == roleName);
                        if (role == null)
                        {
                            throw new ArgumentException("Given role does not exist in store: " + roleName);
                        }

                        var roleAuth = role.Authorizations.FirstOrDefault(t => t.SID.StringValue == identity.GetUserBinarySSid());
                        if (roleAuth == null)
                        {
                            throw new ArgumentException(String.Format("Authorization not found for user {0} in role {1}", userName, roleName));
                        }

                        roleAuth.Delete();
                    }


                });

        }

        public void AddOperationToRole(string roleName, string operationName)
        {
            ExecuteStorageTransation(
                () =>
                {
                    var application = _azManStorage[_storeName][_applicationName];

                    var operation = application.GetItems(ItemType.Operation).FirstOrDefault(t => t.Name == operationName);
                    if (operation == null)
                    {
                        throw new ArgumentException("Given operation does not exist in store: " + operationName);
                    }

                    var role = application.GetItems(ItemType.Role).FirstOrDefault(t => t.Name == roleName);
                    if (role == null)
                    {
                        throw new ArgumentException("Given role does not exist in store: " + roleName);
                    }

                    role.AddMember(operation);
                });
        }

        public void RemoveOperationFromRole(string roleName, string operationName)
        {
            ExecuteStorageTransation(
                () =>
                {
                    var application = _azManStorage[_storeName][_applicationName];

                    var operation = application.GetItems(ItemType.Operation).FirstOrDefault(t => t.Name == operationName);
                    if (operation == null)
                    {
                        throw new ArgumentException("Given operation does not exist in store: " + operationName);
                    }

                    var role = application.GetItems(ItemType.Role).FirstOrDefault(t => t.Name == roleName);
                    if (role == null)
                    {
                        throw new ArgumentException("Given role does not exist in store: " + roleName);
                    }

                    role.RemoveMember(operation);
                });
        }

        public Dictionary<int, string> GetRoleList()
        {
            var application = _azManStorage[_storeName][_applicationName];
            return application.GetItems(ItemType.Role).ToDictionary(x => x.ItemId, x => x.Name);
        }

        public Dictionary<int, string> GetOperationList()
        {
            var application = _azManStorage[_storeName][_applicationName];
            return application.GetItems(ItemType.Operation).ToDictionary(x => x.ItemId, x => x.Name);

        }

        public string GetDescription(string role)
        {
            var application = _azManStorage[_storeName][_applicationName];
            return application.GetItem(role).Description;
        }


        public AzManItemDataContract GetById(int id)
        {
            TypeOfAzManItem _typeOfAzManItem;
            AzManItemDataContract azMan;

            var application = _azManStorage[_storeName][_applicationName];
            var keys = application.Items.Values;
            Console.WriteLine("the value of Keys is " + keys + "\n");

            foreach (var key in keys)
            {
                Console.WriteLine("the value of Keys is " + keys + "\n");
                if (key.ItemId == id)
                {

                    var name = key.Name;
                    var description = key.Description;
                    var identifier = key.ItemId;
                    var type = key.ItemType;

                    if (type.ToString() == TypeOfAzManItem.Role.ToString())
                    {
                        _typeOfAzManItem = TypeOfAzManItem.Role;
                    }

                    else if (type.ToString() == TypeOfAzManItem.Operation.ToString())
                    {
                        _typeOfAzManItem = TypeOfAzManItem.Operation;
                    }

                    else
                    {
                        return null;
                    }

                    azMan = new AzManItemDataContract() { Id = identifier, Name = name, Type = _typeOfAzManItem, Description = description };
                    return azMan;

                }

            }
            return null;

        }

        public void SaveItem(AzManItemDataContract item)
        {
            var application = _azManStorage[_storeName][_applicationName];

            

            if (item.Id <= 0)
            {
                if (item.Type.ToString() ==ItemType.Role.ToString())
                {
                    application.CreateItem(item.Name, item.Description, ItemType.Role);
                }
                else if (item.Type.ToString() == ItemType.Operation.ToString())
                {
                    application.CreateItem(item.Name, item.Description, ItemType.Operation);
                }
            }

            else
            {
                //updating an old one;
                var oldAzmanItem = GetById(item.Id);
                if (item.Name != oldAzmanItem.Name && item.Description != oldAzmanItem.Description)
                {
                    application.GetItem(oldAzmanItem.Name).Rename(item.Name);
                    application.GetItem(item.Name).Update(item.Description);
                }

                else if (item.Name != oldAzmanItem.Name)
                {
                    application.GetItem(oldAzmanItem.Name).Rename(item.Name);
                }
                else if (item.Description != oldAzmanItem.Description)
                {
                    application.GetItem(oldAzmanItem.Name).Update(item.Description);
                }

            }


        }

        public void DeleteItem(AzManItemDataContract item)
        {
            var application = _azManStorage[_storeName][_applicationName];
            var deleted= application.GetItem(item.Name);
            application.GetItem(item.Name).Delete();



        }


        #endregion

        #region Private Methods

        private void ExecuteStorageTransation(Action storageUpdateAction)
        {
            try
            {
                _azManStorage.OpenConnection();
                _azManStorage.BeginTransaction();

                storageUpdateAction();

                _azManStorage.CommitTransaction();
            }
            catch
            {
                if (_azManStorage.TransactionInProgress)
                {
                    _azManStorage.RollBackTransaction();
                }

                throw;
            }
            finally
            {
                _azManStorage.CloseConnection();
            }
        }




        private WindowsIdentity GetIdentityFromUserName(string userName)
        {
            WindowsIdentity identity;

            string email = userName.Replace("@humanarc.com", string.Empty).Replace("ACOR\\", string.Empty) + "@humanarc.com";

            try
            {
                identity = new WindowsIdentity(email);
            }
            catch (Exception ex)
            {
                throw new Exception("Error loading windows identity for username: " + userName, ex);
            }

            return identity;
        }

        /// <summary>
        /// If the username is bob@somewhere.com, and not just bob, and not bob@humanarc.com, then it should be a DB user, from the 
        /// Web Portal user database.
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        private IAzManDBUser GetIdentityFromUserDatabase(string userName)
        {
            if (!userName.Contains("@") || userName.ToLower().Contains("@humanarc.com"))
            {
                return (IAzManDBUser)null;		//its an ACOR windows user, return null
            }

            IAzManDBUser dbUser = (IAzManDBUser)null;
            dbUser = _azManStorage.GetDBUser(userName);
            if (dbUser == null)
            {
                throw new Exception(string.Format("User should be able to be found in the db and was not {0} {1} Check rights to account being used to access the Table Function [dbo].[netsqlazman_GetDBUsers]"
                    , userName
                    , _azManStorage.ConnectionString));
            }
            return dbUser;

        }



        #endregion
    }
}
