using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanArcCompliance.helpers;
using System.DirectoryServices.AccountManagement;
using System.Configuration;
using HumanArcCompliance.Models;
using Microsoft.Ajax.Utilities;

namespace HumanArcCompliance.Controllers
{
    public class TrainingController : Controller
    {
        sessionStorage session = new sessionStorage();
        public ActionResult MyTraining()
        {

            //get list of quizes assigned to user and return
            return View(session.getSessionVars());
        }

        public ActionResult GetAllUsers()
        {
            Queries q = new Queries();
            List<User> allUsers = q.getAllUsers();
            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult AddQuiz()
        //{
        //    HumanArcEntities dbContext = new HumanArcEntities();

        //}
        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //group comment start
        // sean uncomment start
        //public ActionResult ManageEmployees()
        //{
        //    if (checkUserAuth("manager"))
        //    {
        //        if (session.getSessionVars() != null)
        //        {
        //            return View(session.getSessionVars());
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        public ActionResult GetAllManagersUsers()
        {
            sessionStorage sessUser = new sessionStorage();
            ADSearcher ad = new ADSearcher();
            UserViewModel currentUser = new UserViewModel();
            List<User> allUsers = new List<User>();
            currentUser = sessUser.getSessionVars();
            allUsers = ad.getDirectReports(currentUser);
            return Json(allUsers, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult EditTraining()
        //{
        //    if (checkUserAuth("hr"))
        //    {
        //        if (session.getSessionVars() != null)
        //        {
        //            return View(session.getSessionVars());
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        //public ActionResult AddTraining()
        //{
        //    if (checkUserAuth("hr"))
        //    {
        //        if (session.getSessionVars() != null)
        //        {
        //            return View(session.getSessionVars());
        //        }
        //    }
        //    return RedirectToAction("Index", "Home");
        //}
        //group comment end
        //sean uncomment end

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // sean comment start
        // group uncomment start
        public ActionResult ManageEmployees()
        {

            //get employees 
            return View(session.getSessionVars());

        }


        public ActionResult EditTraining()
        {
            return View(session.getSessionVars());


        }
        public ActionResult AddTraining()
        {

            return View(session.getSessionVars());

        }
        // sean comment end
        //group uncomment end

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Quiz()
        {
            return View();
        }
        public JsonResult QuizQuestionAns()
        {
            List<Questionsoptions> obj = new List<Questionsoptions>();
            obj.Add(new Questionsoptions
            {
                Question = "What is 12+20?",
                OpA = "21",
                OpB = "32",
                OpC = "41",
                OpD = "12",
                Ans = "B"
            });
            obj.Add(new Questionsoptions
            {
                Question = "What is 12+12?",
                OpA = "10",
                OpB = "12",
                OpC = "24",
                OpD = "12",
                Ans = "C"
            });
            obj.Add(new Questionsoptions
            {
                Question = "What is 12+24?",
                OpA = "36",
                OpB = "24",
                OpC = "12",
                OpD = "12",
                Ans = "A"
            });
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        /// ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // group comment start
        //sean uncomment start
        //public bool checkUserAuth(string type)
        //{
        //    String hr = (ConfigurationManager.AppSettings["hrGroup"]);
        //    String manager = (ConfigurationManager.AppSettings["managers"]);
        //    ADSearcher searcher = new ADSearcher();
        //    UserPrincipal user = searcher.findCurrentUserName(Request);
        //    using (var context = new PrincipalContext(ContextType.Domain))
        //    {
        //        if (type == "manager")
        //        {
        //            try
        //            {
        //                if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, hr)) || user.IsMemberOf(GroupPrincipal.FindByIdentity(context, manager)))
        //                {
        //                    return true;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);
        //            }
        //        }
        //        else if (type == "hr")
        //        {
        //            try
        //            {
        //                if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, hr)))
        //                {
        //                    return true;
        //                }
        //            }
        //            catch (Exception e)
        //            {
        //                Console.WriteLine(e);
        //            }
        //        }
        //    }
        //    return true;
        //}
        // group comment end
        //sean uncomment end

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult gradeQuiz()
        {
            return View(session.getSessionVars());

        }
    }
    public class Questionsoptions
    {
        public string Question { get; set; }
        public string OpA { get; set; }
        public string OpB { get; set; }
        public string OpC { get; set; }
        public string OpD { get; set; }
        public string Ans { get; set; }
    }
}