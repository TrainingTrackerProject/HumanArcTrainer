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
        public ActionResult Index(string error = "")
        {
            if (error != "")
            {
                ViewBag.errorMessage = error;
            }

            sessionStorage session = new sessionStorage();
            if (session.getSessionUser() != null)
            {
                return View(session.getSessionUser());
            }

            validation val = new validation();

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            //Sean Uncomment Start
            //Group Comment Start

            //if (val.getUserCredentials(Request))
            //{
            //    return View(session.getSessionUser());
            //}
            //return RedirectToAction("Login");

            //Sean Uncomment End
            //Group Comment End

            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            //sean comment start
            //group uncomment start

            User myUser = new User();
            myUser = val.validate();
            UserViewModel vmUser = myUser.userToModel(myUser);
            vmUser.isHR = true;
            session.setSessionUser(vmUser);
            return View(session.getSessionUser());

            //sean comment end
            //group uncomment end


            /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////            

        }

        public ActionResult Login()
        {
            return View();
        }

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

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }

}