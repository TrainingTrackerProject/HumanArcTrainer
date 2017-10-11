using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanArcCompliance.Models;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using HumanArcCompliance.helpers;
using System.Configuration;
namespace HumanArcCompliance.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// determines if the user is allowed to view certain elements of the page
        /// checks if active directory is updated accouring to the last log entry 
        /// </summary>
        /// <param name="req"></param>
        /// <returns>List of all managers whose last name CONTAINS the value given</returns>
        public ActionResult Index()
        {
            //ApplicationDbContext will be user when a user logs in a new user entry is created for them
            //ApplicationDbContext db = new ApplicationDbContext();
            ADSearcher ad = new ADSearcher();
            ADUser myADUser = new ADUser();
            String managers = (ConfigurationManager.AppSettings["managers"]);
            String hrGroup = (ConfigurationManager.AppSettings["HRGroup"]);
            UserPrincipal user = ad.findCurrentUserName(Request);
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                try
                {
                    if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, managers)))
                    {
                        myADUser = ad.findByUserName(user);
                        myADUser.isManager = "true";
                    }
                    else if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, hrGroup)))
                    {
                        myADUser = ad.findByUserName(user);
                        myADUser.isHR = "true";
                    }
                    else
                    {
                        myADUser = ad.findByUserName(user);
                        myADUser.isManager = "false";
                        myADUser.isHR = "false";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    myADUser = ad.findByUserName(user);
                    myADUser.isManager = "false";
                    myADUser.isHR = "false";
                }
            }
            ad.setSessionVars(myADUser);

            return View(myADUser);
        }  
    }
}