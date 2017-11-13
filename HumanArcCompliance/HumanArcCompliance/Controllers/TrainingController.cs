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
using static HumanArcCompliance.Models.AddQuizDeserializer;
using static HumanArcCompliance.Models.SubmitQuizDeserializer;

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
            List<ManageEmployeeViewModel> userQuizes = new List<ManageEmployeeViewModel>();
            Queries q = new Queries();
            List<User> Users = q.getAllUsers();
            foreach(User user in Users)
            {
                ManageEmployeeViewModel userQuiz = new ManageEmployeeViewModel();
                userQuiz.id = user.id;
                userQuiz.firstName = user.firstName;
                userQuiz.lastName = user.lastName;
                userQuiz.email = user.email;
                userQuiz.manager = user.manager;
                userQuiz.SAMAccountName = user.SAMAccountName;
                userQuiz.hasUngradedQuiz = false;
                List<UserQuizQuestionAnswer> usersQuizes = q.getAllUserQuizes(user.id);
                foreach(UserQuizQuestionAnswer uqqa in usersQuizes)
                {
                    if ((bool)!uqqa.isChecked)
                    {
                        userQuiz.hasUngradedQuiz = true;
                    }
                }
                userQuizes.Add(userQuiz);
            }
            return Json(userQuizes, JsonRequestBehavior.AllowGet);
        }

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
        //
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

        public ActionResult Quiz(int id)
        {
            ViewBag.id = id;
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
            var result = JsonConvert.DeserializeObject<JQuiz>(j);           
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

        [HttpGet]
        public ActionResult GetUserQuizes()
        {
            Queries query = new Queries();
            sessionStorage session = new sessionStorage();
            ViewModel vm = new ViewModel();
            User user = query.getUserBySam(vm.modelToUser(session.getSessionVars()).SAMAccountName);
            List<EmployeeQuizesViewModel> employeeQuizes = new List<EmployeeQuizesViewModel>();
            string[] groups = user.userGroups.Split(',');
            foreach (string group in groups)
            {
                int groupId = query.getGroupByName(group).id;
                List<Quize> quizesByGroup = query.getQuizesByGroupId(groupId);
                foreach (Quize quiz in quizesByGroup)
                {
                    EmployeeQuizesViewModel employeeQuiz = new EmployeeQuizesViewModel();
                    employeeQuiz.userId = user.id;
                    employeeQuiz.firstName = user.firstName;
                    employeeQuiz.lastName = user.lastName;
                    employeeQuiz.quizId = quiz.id;
                    employeeQuiz.quizTitle = quiz.title;
                    employeeQuiz.isCompleted = false;
                    employeeQuiz.isGraded = false;
                    UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                    uqqa = query.getQuizByUserIdQuizId(user.id, quiz.id);
                    if (uqqa.id != 0)
                    {
                        employeeQuiz.isCompleted = true;
                        //if ( (bool)uqqa.isChecked!=null && (bool)uqqa.isChecked)
                        //{
                        //    employeeQuiz.isGraded = true;
                        //}
                    }

                    employeeQuizes.Add(employeeQuiz);

                }
            }
            return Json(employeeQuizes, JsonRequestBehavior.AllowGet);
        }



        [HttpPost]
        public ActionResult GetUserQuizes(string id)
        {
            Queries query = new Queries();         
            User user = query.getUserById(Convert.ToInt32(id));          
            List<EmployeeQuizesViewModel> employeeQuizes = new List<EmployeeQuizesViewModel>();
            string[] groups = user.userGroups.Split(',');
            foreach(string group in groups)
            {
                int groupId = query.getGroupByName(group).id;
                List<Quize> quizesByGroup = query.getQuizesByGroupId(groupId);
                foreach(Quize quiz in quizesByGroup)
                {
                    EmployeeQuizesViewModel employeeQuiz = new EmployeeQuizesViewModel();
                    employeeQuiz.userId = user.id;
                    employeeQuiz.firstName = user.firstName;
                    employeeQuiz.lastName = user.lastName;
                    employeeQuiz.quizId = quiz.id;
                    employeeQuiz.quizTitle = quiz.title;
                    employeeQuiz.isCompleted = false;
                    employeeQuiz.isGraded = false;
                    UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                    uqqa = query.getQuizByUserIdQuizId(user.id, quiz.id);
                    if (uqqa.id != 0)
                    {
                        employeeQuiz.isCompleted = true;
                        //if ((bool)uqqa.isChecked)
                        //{
                        //    employeeQuiz.isGraded = true;
                        //}
                    }

                    employeeQuizes.Add(employeeQuiz);
                    
                }
            }
            
            return Json(employeeQuizes, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult EmployeeQuizes(int id)
        {
            ViewBag.id = id;
            return View();
        }

        public ActionResult GetAllQuizes()
        {
            Queries query = new Queries();
            List<Quize> quizes = query.getAllQuizes();
            List<Quize> sending = new List<Quize>();
            foreach(Quize quiz in quizes)
            {
                Quize q = new Quize();
                q.id = quiz.id;
                q.title = quiz.title;
                q.description = quiz.description;
                sending.Add(q);
            }
            return Json(sending, JsonRequestBehavior.AllowGet);
        }
        public void RemoveQuiz(string id)
        {
            Queries query = new Queries();
            query.RemoveQuiz(Convert.ToInt32(id));
        }
        public ActionResult GetQuizById(string id)
        {
            int quizId = Convert.ToInt32(id);
            Queries query = new Queries();
            Quize quiz = query.getQuizById(quizId);

            QuizViewModel qvmQuiz = new QuizViewModel();
            qvmQuiz.id = quizId;
            qvmQuiz.title = quiz.title;
            qvmQuiz.description = quiz.description;
            qvmQuiz.questions = new List<QVMQuestion>();
            List<Question> questions = query.getQuestionsByQuiz(quiz.id);

            foreach(Question question in questions)
            {
                QVMQuestion qvmQuestion = new QVMQuestion();
                qvmQuestion.id = question.id;
                qvmQuestion.text = question.questionText;
                qvmQuestion.type = question.questionType;
                qvmQuestion.answers = new List<QVMAnswer>();
                List<Answer> answers = query.getAnswersByQuestion(question.id);

                foreach (Answer answer in answers)
                {
                    QVMAnswer qvmAnswer = new QVMAnswer();
                    qvmAnswer.id = answer.id;
                    qvmAnswer.answerText = answer.answerText;
                    qvmAnswer.isCorrect = answer.isCorrect;
                    qvmQuestion.answers.Add(qvmAnswer);
                }
                qvmQuiz.questions.Add(qvmQuestion);
            }
            return Json(qvmQuiz, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SubmitQuiz(string data)
        {
            Queries query = new Queries();
            sessionStorage session = new sessionStorage();
            ViewModel vm = new ViewModel();
            User user = query.getUserBySam(vm.modelToUser(session.getSessionVars()).SAMAccountName);
            var result = JsonConvert.DeserializeObject<SubmitQuiz>(data);
            List<UserQuizQuestionAnswer> uqqas = new List<UserQuizQuestionAnswer>();
            foreach(SubmitQuestions question in result.questions)
            {
                foreach(SubmitAnswers answer in question.answers)
                {
                    UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                    uqqa.quizId = result.id;
                    uqqa.questionId = question.id;
                    uqqa.answerId = answer.id;
                    uqqa.userId = user.id;
                    if (answer.text != null)
                    {
                        uqqa.text = answer.text;
                    }
                    uqqas.Add(uqqa);
                }
            }
            query.submitQuiz(uqqas);


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

}