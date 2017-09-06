using System;
using System.Collections.Generic;
using System.Configuration;
using HumanArc.Compliance.Shared.Enumerations;
using HumanArc.Foundation.Data.DataContracts;
using HumanArc.Foundation.Security;

namespace HumanArc.Compliance.Data
{
    public class AuthorizationDA
    {
        private const string StoreName = "Compass";
        private const string AppName = "Compliance";
        private readonly NetSqlAuthorization _netSqlAzMan;

        public AuthorizationDA()
        {
            _netSqlAzMan = new NetSqlAuthorization(ConfigurationManager.ConnectionStrings["NetSqlAzMan"].ConnectionString, StoreName, AppName);
        }

        public bool IsAuthorized(string userName, SecuredOperationEnum operation)
        {
            return _netSqlAzMan.Authorize(userName, operation.ToString());
        }

        public bool IsAuthorized(string userName, string operationName)
        {
            return _netSqlAzMan.Authorize(userName, operationName);
        }

        public List<SecuredOperationEnum> GetAllOperationsForUser(string userName)
        {
            var outOpNames = new List<SecuredOperationEnum>();
            var opNames = _netSqlAzMan.GetOperationsForUser(userName);
            foreach (var op in opNames)
            {
                SecuredOperationEnum securedOperationEnum;
                if (Enum.TryParse(op, out securedOperationEnum))
                {
                    outOpNames.Add(securedOperationEnum);
                }
            }
            return (outOpNames);
        }

        public List<string> GetAllRolesForUser(string userName)
        {
            return _netSqlAzMan.GetRolesForUser(userName);
        }

        public void AddUserToRole(string roleName, string userName)
        {
            _netSqlAzMan.AddUserToRole(roleName, userName);
        }

        public void DeleteUserFromRole(string roleName, string userName)
        {
            _netSqlAzMan.RemoveUserFromRole(roleName, userName);
        }

        public List<string> GetAllRoles()
        {
            return _netSqlAzMan.GetAllRoles();
        }

        public Dictionary<int, string> GetRoleList()
        {
            return _netSqlAzMan.GetRoleList();
        }

        public Dictionary<string, List<AzmanAuthorizationDataContract>> GetRolesAndUsers()
        {
            return _netSqlAzMan.GetRolesAndUsers();
        } 

        public void SaveItem(AzManItemDataContract item)
        {
            _netSqlAzMan.SaveItem(item);
        }

        public AzManItemDataContract GetById(int id)
        {
            return _netSqlAzMan.GetById(id);
        }

        public Dictionary<int, string> GetOperationList()
        {
            return _netSqlAzMan.GetOperationList();
        }

        public string GetDescription(string role)
        {
            return _netSqlAzMan.GetDescription(role);
        }

        public void DeleteItem(AzManItemDataContract item)
        {
            _netSqlAzMan.DeleteItem(item);
        }
    }
}

