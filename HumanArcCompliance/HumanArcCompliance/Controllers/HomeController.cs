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
using System.Data.SqlClient;
using System.Data;

namespace HumanArcCompliance.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// determines if the user is allowed to view certain elements of the page
        /// checks if active directory is updated accouring to the last log entry 
        /// </summary>
        /// <param name="req"></param>
        public ActionResult Index()
        {
<<<<<<< HEAD
            //ApplicationDbContext will be user when a user logs in a new user entry is created for them
            //ApplicationDbContext db = new ApplicationDbContext();
            //ADSearcher ad = new ADSearcher();
=======
            sessionStorage session = new sessionStorage();
            if (session.getSessionVars() != null)
            {
                return View(session.getSessionVars());
            }
>>>>>>> 047c5b76838955201fd965ff4d476d3464ae2c00
            ADUser myADUser = new ADUser();
            // Stored in config files so Human Arc can change to meet their group names
            String managers = (ConfigurationManager.AppSettings["managers"]);
            String hrGroup = (ConfigurationManager.AppSettings["HRGroup"]);
<<<<<<< HEAD
            // Call to ADSearcher
            //UserPrincipal user = ad.findCurrentUserName(Request);
            /*using (var context = new PrincipalContext(ContextType.Domain))
=======
            
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //group comment start
            //sean uncomment start
            //ApplicationDbContext will be user when a user logs in a new user entry is created for them

            ADSearcher ad = new ADSearcher();


            UserPrincipal user = ad.findCurrentUserName(Request);
            using (var context = new PrincipalContext(ContextType.Domain))
>>>>>>> 047c5b76838955201fd965ff4d476d3464ae2c00
            {
                try
                {
                    //myADUser = ad.findByUserName(user);
                    //if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, hrGroup)))
                    {
                        myADUser.isHR = "true";
                        myADUser.isManager = "false";

                    }
                    //else if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, managers)))
                    {
                        myADUser.isHR = "false";
                        myADUser.isManager = "true";
<<<<<<< HEAD
                    }                
                    //else
=======
                    }
                    else
>>>>>>> 047c5b76838955201fd965ff4d476d3464ae2c00
                    {
                        myADUser.isManager = "false";
                        myADUser.isHR = "false";
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    //***Not*** imlemented yet if user info fails to be pulled go to login page
                    //return RedirectToAction("login", "LoginController");

                }
<<<<<<< HEAD
            }*/
            //ad.setSessionVars(myADUser);
=======
            }
            //group comment end
            //sean uncomment end

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //sean comment start
            //group uncomment start
            //validateUser validation = new validateUser();
            //myADUser = validation.validate();

            //sean comment end
            //group uncomment end
>>>>>>> 047c5b76838955201fd965ff4d476d3464ae2c00

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Queries query = new Queries();
            query.checkExistingUser(myADUser);
            session.setSessionVars(myADUser);
            return View(myADUser);
        }  
    }
}