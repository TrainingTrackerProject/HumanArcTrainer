using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HumanArcCompliance.Controllers
{
    public class TrainingController : Controller
    {
        public ActionResult MyTraining()
        {
            return View();
        }

        public ActionResult ManageEmployees()
        {
            return View();
        }

        public ActionResult EditTraining()
        {
            return View();
        }
        public ActionResult AddTraining()
        {
            return View();
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