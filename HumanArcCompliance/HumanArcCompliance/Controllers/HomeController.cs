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
        public ActionResult Index(string logonUser = null)
        {
            ADUser myADUser = new ADUser();
            sessionStorage session = new sessionStorage();
            if (logonUser != null)
            {
                myADUser = (ADUser)TempData[logonUser];
            }
            else
            {
                if (session.getSessionVars() != null)
                {
                    return View(session.getSessionVars());
                }
            }
         



            // Stored in config files so Human Arc can change to meet their group names
            String managers = (ConfigurationManager.AppSettings["managers"]);
            String hrGroup = (ConfigurationManager.AppSettings["HRGroup"]);

            
            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            /*group comment start
            //sean uncomment start
            //ApplicationDbContext will be user when a user logs in a new user entry is created for them

            //ADSearcher ad = new ADSearcher();



            /*UserPrincipal user = ad.findCurrentUserName(Request);
            using (var context = new PrincipalContext(ContextType.Domain))

            {
                try
                {
                    //myADUser = ad.findByUserName(user);
                    if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, hrGroup)))
                    {
                        myADUser.isHR = "true";
                        myADUser.isManager = "false";

                    }
                    else if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, managers)))
                    {
                        myADUser.isHR = "false";
                        myADUser.isManager = "true";

                    }
                    else
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

            //    }
            //}

            
            //group comment end
            //sean uncomment end

            ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //sean comment start
            //group uncomment start
            //validateUser validation = new validateUser();
            //myADUser = validation.validate();

            //sean comment end
            //group uncomment end
            */

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            Queries query = new Queries();
            //query.checkExistingUser(myADUser);
            session.setSessionVars(myADUser);
            return View(myADUser);
        }
        //public ActionResult Login()
        //{
        //    return View();
        //}

        ////////////////////////////////////////////////////////////////////////////////////////////////////////

        //Sean Uncomment start
        //group comment start
        public ActionResult Login(string username = "", string password = "")
        {
            if (username != "" && password != "")
            {
                using (var context = new PrincipalContext(ContextType.Domain))
                {
                    ADSearcher ad = new ADSearcher();
                    if (ad.IsAuthenticated(username, password))
                    {
                        UserPrincipal user = ad.findSearchedUserName(username);

                        TempData["logonUser"] = ad.findByUserName(user);
                        return RedirectToAction("Index", new { logonUser = "logonUser" });
                    }
                }
                ViewBag.userMessage = "Access Denied Invalid Login Please Try Again";
                return View();
            }
            ViewBag.userMessage = "Please Log In";
            return View();          
        }

        //group comment end
        //sean uncomment end

        // /////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}