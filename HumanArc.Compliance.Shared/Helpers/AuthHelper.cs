using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HumanArc.Compliance.Shared.Helpers
{
    public static class AuthHelper
    {
        public static string GetUserName()
        {
            if (HttpContext.Current != null
                && HttpContext.Current.User != null
                && !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
                return HttpContext.Current.User.Identity.Name;

            return null;
        }
    }
}
