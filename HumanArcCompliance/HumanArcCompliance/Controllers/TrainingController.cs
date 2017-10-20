using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HumanArcCompliance.helpers;
using System.DirectoryServices.AccountManagement;
using System.Configuration;


namespace HumanArcCompliance.Controllers
{
    public class TrainingController : Controller
    {
        public ActionResult MyTraining()
        {
            //get list of quizes assigned to user and return
            return View();
        }

        public ActionResult ManageEmployees()
        {
            if (checkUserAuth("manager"))
            {
                //get employees 
                return View();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult EditTraining()
        {
            if (checkUserAuth("hr"))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
        public ActionResult AddTraining()
        {
            if (checkUserAuth("hr"))
            {
                return View();
            }
            return RedirectToAction("Index", "Home");
        }
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
                Ans = "B"
            });
            obj.Add(new Questionsoptions
            {
                Question = "What is 12+12?",
                OpA = "10",
                OpB = "12",
                OpC = "24",
                Ans = "C"
            });
            obj.Add(new Questionsoptions
            {
                Question = "What is 12+24?",
                OpA = "36",
                OpB = "24",
                OpC = "12",
                Ans = "A"
            });
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public bool checkUserAuth(string type)
        {
            String hr = (ConfigurationManager.AppSettings["hrGroup"]);
            String manager = (ConfigurationManager.AppSettings["managers"]);
            ADSearcher searcher = new ADSearcher();
            UserPrincipal user = searcher.findCurrentUserName(Request);
            using (var context = new PrincipalContext(ContextType.Domain))
            {
                if (type == "manager")
                {
                    try
                    {                 
                        if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, hr)) || user.IsMemberOf(GroupPrincipal.FindByIdentity(context, manager)))
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
                else if(type == "hr")
                {
                    try
                    {
                        if (user.IsMemberOf(GroupPrincipal.FindByIdentity(context, hr)))
                        {
                            return true;
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }                   
            }
            return false;
        }
    }
    public class Questionsoptions
    {
        public string Question { get; set; }
        public string OpA { get; set; }
        public string OpB { get; set; }
        public string OpC { get; set; }
        public string Ans { get; set; }
    }
}