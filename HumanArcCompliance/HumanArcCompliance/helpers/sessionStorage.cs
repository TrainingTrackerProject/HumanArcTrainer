using HumanArcCompliance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.helpers
{
    public class sessionStorage
    {
        public void setSessionUser(UserViewModel vmUser)
        {
            HttpContext.Current.Session["currentUser"] = vmUser;
        }


        public UserViewModel getSessionUser()
        {
            UserViewModel vmUser = (UserViewModel)HttpContext.Current.Session["currentUser"];
            return vmUser;
        }
    }
}