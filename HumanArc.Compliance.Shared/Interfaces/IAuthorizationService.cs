using System;
using System.Collections.Generic;
using HumanArc.Compliance.Shared.Enumerations;
using HumanArc.Compliance.Shared.ViewModels;
using HumanArc.Foundation.Data.DataContracts;

namespace HumanArc.Compliance.Shared.Interfaces
{
    public interface IAuthorizationService
    {
        IList<string> GetRoles(string userName);
        Dictionary<string, List<AzmanAuthorizationDataContract>> GetRolesAndUsers();
        Dictionary<string, List<AzmanAuthorizationDataContract>> Assign(RoleAssignentViewModel contract);
        Dictionary<string, List<AzmanAuthorizationDataContract>> Remove(RoleAssignentViewModel contract);
        bool IsAuthorized(string userName, SecuredOperationEnum operation);
    }
}
