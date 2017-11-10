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
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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

        public ActionResult GetAllGroups()
        {
            Queries q = new Queries();
            List<Group> groups = new List<Group>();
            groups = q.getAllGroups();
            List<Group> sending = new List<Group>();
            foreach(Group group in groups)
            {
                Group g = new Group();
                g.id = group.id;
                g.name = group.name;
                sending.Add(g);
            }
            return Json(sending, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddQuiz(string group, string j)// string quiz, string question)
        {
            Queries query = new Queries();
            string[] groups = group.Split(',');
            var result = JsonConvert.DeserializeObject<Quiz>(j);           
            foreach(string g in groups)
            {
                Quize quiz = new Quize();
                quiz.description = result.description;
                quiz.title = result.title;
                quiz.groupId = Convert.ToInt32(g);
                int quizId = query.addQuiz(quiz);
                foreach (var question in result.questions)
                {
                    Question q = new Question();
                    q.questionType = question.type;
                    q.questionText = question.text;
                    q.quizId = quizId;
                    int questionId = query.addQuestion(q);
                    foreach (var answer in question.answers)
                    {
                        Answer ans = new Answer();
                        ans.answerText = answer.answerText;
                        if (answer.isCorrect == "false")
                        {
                            ans.isCorrect = false;
                        }
                        else
                        {
                            ans.isCorrect = true;
                        }
                        ans.questionId = questionId;
                        query.addAnswer(ans);
                    }

                }
            }
            

            return Json("success", JsonRequestBehavior.AllowGet);

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

    public class Answers
    {
        public string answerText { get; set; }
        public string isCorrect { get; set; }
    }

    public class Questions
    {
        public string type { get; set; }
        public string text { get; set; }
        public List<Answers> answers { get; set; }
    }

    public class Quiz
    {
        public string title { get; set; }
        public string description { get; set; }
        public List<Questions> questions { get; set; }


    }

    public class RootObject
    {
        public List<Quiz> result { get; set; }
    }
}