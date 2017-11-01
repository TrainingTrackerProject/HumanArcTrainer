using HumanArcCompliance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.helpers
{
    public class sessionStorage
    {
        public void setSessionVars(ADUser myADUser)
        {
            HttpContext.Current.Session["currentUser"] = myADUser;
        }


        public ADUser getSessionVars()
        {
            ADUser myADUser = (ADUser)HttpContext.Current.Session["currentUser"];
            return myADUser;
        }
    }
}