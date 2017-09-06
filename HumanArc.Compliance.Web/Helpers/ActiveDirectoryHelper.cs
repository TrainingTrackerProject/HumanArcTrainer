using System.Collections.Generic;
using System.Web;
using System.DirectoryServices.AccountManagement;

namespace HumanArc.Compliance.Web.Helpers
{
    public static class ActiveDirectoryHelper
    {

        public static string GetCurrentUsersName()
        {
            return HttpContext.Current.User.Identity.Name;
            //return System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString();
        }

        public static Dictionary<string,string> GetADGroups()
        {
            var listOfGroup = new Dictionary<string, string>();
            PrincipalContext ctx = new PrincipalContext(ContextType.Domain);

            // define a "query-by-example" principal - here, we search for a GroupPrincipal 
            GroupPrincipal qbeGroup = new GroupPrincipal(ctx);

            // create your principal searcher passing in the QBE principal    
            PrincipalSearcher srch = new PrincipalSearcher(qbeGroup);

            // find all matches
            foreach (var found in srch.FindAll())
            {
                if (found.DisplayName != null)
                {
                    listOfGroup.Add(found.DisplayName, found.DisplayName);
                }        
            }
            return listOfGroup;
        }
    }
}