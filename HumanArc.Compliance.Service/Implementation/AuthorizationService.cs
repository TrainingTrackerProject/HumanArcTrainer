using System;
using System.Collections.Generic;
using HumanArc.Compliance.Data;
using HumanArc.Compliance.Shared.Enumerations;
using HumanArc.Compliance.Shared.Interfaces;
using HumanArc.Compliance.Shared.ViewModels;
using HumanArc.Foundation.Data.DataContracts;

namespace HumanArc.Compliance.Service.Implementation
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly AuthorizationDA _authDa;

        public AuthorizationService(AuthorizationDA authDA)
        {
            _authDa = authDA;
        }

        public IList<string> GetRoles(string userName)
        {
            return _authDa.GetAllRoles();
        }

        public Dictionary<string, List<AzmanAuthorizationDataContract>> GetRolesAndUsers()
        {
            return _authDa.GetRolesAndUsers();
        }

        public Dictionary<string, List<AzmanAuthorizationDataContract>> Assign(RoleAssignentViewModel contract)
        {
            _authDa.AddUserToRole(contract.Role, contract.UserName);
            return _authDa.GetRolesAndUsers();
        }

        public Dictionary<string, List<AzmanAuthorizationDataContract>> Remove(RoleAssignentViewModel contract)
        {
            _authDa.DeleteUserFromRole(contract.Role, contract.UserName);
            return _authDa.GetRolesAndUsers();
        }

        public bool IsAuthorized(string userName, SecuredOperationEnum operation)
        {
            return _authDa.IsAuthorized(userName, operation);
        }
    }
}
