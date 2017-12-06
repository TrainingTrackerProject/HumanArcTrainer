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
using static HumanArcCompliance.Models.Deserializers;
using static HumanArcCompliance.Models.SubmitQuizDeserializer;
using static HumanArcCompliance.Models.Serializers;

namespace HumanArcCompliance.Controllers
{
    public class TrainingController : Controller
    {
        sessionStorage session = new sessionStorage();
        validation val = new validation();
        String managers = (ConfigurationManager.AppSettings["managers"]);
        String hrGroup = (ConfigurationManager.AppSettings["HRGroup"]);
        public ActionResult MyTraining()
        {
            if (session.getSessionUser() != null)
            {
                return View(session.getSessionUser());
            }
            else
            {
                if (val.getUserCredentials(Request))
                {
                    return View(session.getSessionUser());
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }
            }
        }

        public ActionResult GetAllUsers()
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }

            List<ManageEmployeeViewModel> userQuizes = new List<ManageEmployeeViewModel>();
            Queries q = new Queries();
            List<User> Users = q.getAllUsers();
            foreach (User user in Users)
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
                foreach (UserQuizQuestionAnswer uqqa in usersQuizes)
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

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        //group comment start
        //sean uncomment start

        //public ActionResult GetAllManagersUsers()
        //{

        //    UserViewModel vmUser = session.getSessionUser();
        //    if (vmUser == null)
        //    {
        //        if (!val.getUserCredentials(Request))
        //        {
        //            return RedirectToAction("Login", "Home");
        //        }
        //        vmUser = session.getSessionUser();
        //    }
        //    if (!val.checkUserAuth(vmUser, managers))
        //    {
        //        return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
        //    }
        //    ADSearcher ad = new ADSearcher();
        //    List<User> allUsers = ad.getDirectReports(vmUser);
        //    return Json(allUsers, JsonRequestBehavior.AllowGet);
        //}

        //public ActionResult ManageEmployees()
        //{
        //    UserViewModel vmUser = session.getSessionUser();
        //    if (vmUser == null)
        //    {
        //        if (!val.getUserCredentials(Request))
        //        {
        //            return RedirectToAction("Login", "Home");
        //        }
        //        vmUser = session.getSessionUser();
        //    }
        //    if (!val.checkUserAuth(vmUser, managers))
        //    {
        //        return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
        //    }
        //    return View(vmUser);
        //}

        //public ActionResult AllTraining()
        //{
        //    UserViewModel vmUser = session.getSessionUser();
        //    if (vmUser == null)
        //    {
        //        if (!val.getUserCredentials(Request))
        //        {
        //            return RedirectToAction("Login", "Home");
        //        }
        //        vmUser = session.getSessionUser();
        //    }
        //    if (!val.checkUserAuth(vmUser, hrGroup))
        //    {
        //        return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
        //    }
        //    return View(vmUser);
        //}

        //public ActionResult AddTraining()
        //{
        //    UserViewModel vmUser = session.getSessionUser();
        //    if (vmUser == null)
        //    {
        //        if (!val.getUserCredentials(Request))
        //        {
        //            return RedirectToAction("Login", "Home");
        //        }
        //        vmUser = session.getSessionUser();
        //    }
        //    if (!val.checkUserAuth(vmUser, hrGroup))
        //    {
        //        return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
        //    }
        //    return View(vmUser);
        //}
        //group comment end
        //sean uncomment end

        /// ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        // sean comment start
        // group uncomment start
        public ActionResult ManageEmployees()
        {
            return View(session.getSessionUser());
        }

        public ActionResult AllTraining()
        {
            return View(session.getSessionUser());
        }
        public ActionResult updateTraining()
        {
            return View(session.getSessionUser());
        }
        public ActionResult AddTraining()
        {
            return View(session.getSessionUser());
        }
        // sean comment end
        //group uncomment end

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public ActionResult Quiz(int id = 0)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            Queries query = new Queries();
            if (id == 0)
            {
                return RedirectToAction("Index", "home", new { error = "Cannot Locate Quiz" });
            }
            else if (!vmUser.userGroups.Contains(query.getGroupById(query.getQuizById(id).groupId).name))
            {
                return RedirectToAction("Index", "home", new { error = "Invalid Quiz" });
            }
            UserQuizViewModel uqvmQuiz = GetQuizById(id);
            uqvmQuiz.isHR = vmUser.isHR;
            uqvmQuiz.isManager = vmUser.isManager;
            return View(uqvmQuiz);
        }

        public ActionResult gradeQuiz()
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            return View(vmUser);
        }

        public ActionResult GetAllGroups()
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            Queries q = new Queries();
            List<Group> groups = new List<Group>();
            groups = q.getAllGroups();
            List<Group> sending = new List<Group>();
            foreach (Group group in groups)
            {
                Group g = new Group();
                g.id = group.id;
                g.name = group.name;
                sending.Add(g);
            }
            return Json(sending, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddQuiz(string quizData)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            Queries query = new Queries();
            var result = JsonConvert.DeserializeObject<JQuiz>(quizData);
            if (query.getQuizByTitle(result.title).Count!=0)
            {
                return Json("Duplicate Title", JsonRequestBehavior.AllowGet);
            }
            List<int> quizIds = new List<int>();
            foreach (int group in result.groups)
            {
                Quize quiz = new Quize();
                quiz.groupId = Convert.ToInt32(group);
                quiz.title = result.title;
                quiz.description = result.description;
                quiz.media = result.media;
                quiz.startDate = result.startDate;
                quiz.preferDate = result.preferredDate;
                quiz.expiredDate = result.expirationDate;
                quizIds.Add(query.addQuiz(quiz));
            }
            return Json(quizIds, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpdateQuiz(string quizData)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            Queries query = new Queries();
            var result = JsonConvert.DeserializeObject<JQuiz>(quizData);

            List<int> quizIds = new List<int>();
            List<Quize> quizes = query.getQuizByTitle(result.title);
            List<int> currentGroups = new List<int>();

            foreach(Quize q in quizes)
            {
                if (!result.groups.Contains(q.groupId))
                {
                    query.RemoveQuiz(q.id);
                }
                else
                {
                    quizIds.Add(q.id);

                    q.description = result.description;
                    q.media = result.media;
                    q.startDate = result.startDate;
                    q.preferDate = result.preferredDate;
                    q.expiredDate = result.expirationDate;
                    query.updateExistingQuiz(q);

                    currentGroups.Add(q.groupId);
                }
            }
            foreach(int newGroupId in result.groups)
            {
                if (!currentGroups.Contains(newGroupId)){
                    Quize newQuiz = new Quize();
                    newQuiz.title = result.title;
                    newQuiz.description = result.description;
                    newQuiz.groupId = newGroupId;
                    newQuiz.media = result.media;
                    newQuiz.startDate = result.startDate;
                    newQuiz.preferDate = result.preferredDate;
                    newQuiz.expiredDate = result.expirationDate;
                    quizIds.Add(query.addQuiz(newQuiz));
                }
            }
            
            return Json(quizIds, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddQuizQuestionAnswers(string title, string questionData)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            var result = JsonConvert.DeserializeObject<JQuestion>(questionData);
            Question question = new Question();
            question.questionText = result.questionText;
            question.questionType = result.questionType;

            Queries query = new Queries();

            List<Quize> quizes = query.getQuizByTitle(title);
            List<int> questionIds = new List<int>();
            foreach (Quize quiz in quizes)
            {
                question.quizId = quiz.id;
                int questionId = query.addQuestion(question);
                questionIds.Add(questionId);
               
                if (!(result.questionType == "shortAnswer"))
                {
                    foreach (JAnswers jAnswer in result.answers)
                    {
                        Answer answer = new Answer();
                        answer.questionId = questionId;
                        answer.answerText = jAnswer.answerText;
                        answer.isCorrect = jAnswer.isCorrect;
                        query.addAnswer(answer);
                    }
                }
                else
                {
                    Answer answer = new Answer();
                    answer.questionId = questionId;
                    query.addAnswer(answer);
                }
            }
            return Json(questionIds, JsonRequestBehavior.AllowGet);
        }

        //////////////YO LOOK HERE. THIS IS TODO COPY-PASTED OVER. FIX IT, JAE.
        //////////FOR USERQUIZQUESTIONANSWERS YOU NEED: userId, quizId, questionId, answerId, text, isChecked, isApproved
        public ActionResult AddUserQuizQuestionAnswers(string title, string questionData)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            var result = JsonConvert.DeserializeObject<JQuestion>(questionData);
            Question question = new Question();
            question.questionText = result.questionText;
            question.questionType = result.questionType;

            Queries query = new Queries();

            List<Quize> quizes = query.getQuizByTitle(title);
            List<int> questionIds = new List<int>();
            foreach (Quize quiz in quizes)
            {
                question.quizId = quiz.id;
                int questionId = query.addQuestion(question);
                questionIds.Add(questionId);
                foreach (JAnswers jAnswer in result.answers)
                {
                    Answer answer = new Answer();
                    answer.questionId = questionId;
                    answer.answerText = jAnswer.answerText;
                    answer.isCorrect = jAnswer.isCorrect;
                    query.addAnswer(answer);
                }
            }
            return Json(questionIds, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetUserQuizes()
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            Queries query = new Queries();
            User user = query.getUserBySam(vmUser.modelToUser(session.getSessionUser()).SAMAccountName);
            List<EmployeeQuizesViewModel> employeeQuizes = new List<EmployeeQuizesViewModel>();
            string[] groups = user.userGroups.Split(',');
            foreach (string group in groups)
            {
                int groupId = query.getGroupByName(group).id;
                List<Quize> quizesByGroup = query.getQuizesByGroupId(groupId);
                foreach (Quize quiz in quizesByGroup)
                {
                    if (DateTime.Now.Ticks >= quiz.startDate.Ticks)
                    {
                        EmployeeQuizesViewModel employeeQuiz = new EmployeeQuizesViewModel();
                        employeeQuiz.userId = user.id;
                        employeeQuiz.firstName = user.firstName;
                        employeeQuiz.lastName = user.lastName;
                        employeeQuiz.quizId = quiz.id;
                        employeeQuiz.quizTitle = quiz.title;
                        employeeQuiz.startDate = quiz.startDate;
                        employeeQuiz.preferredDate = quiz.preferDate;
                        employeeQuiz.expirationDate = quiz.expiredDate;
                        employeeQuiz.isCompleted = false;
                        employeeQuiz.isGraded = false;
                        UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                        uqqa = query.getQuizByUserIdQuizId(user.id, quiz.id);
                        if (uqqa.id != 0)
                        {
                            employeeQuiz.isCompleted = true;
                            if (uqqa.isChecked != false && (bool)uqqa.isChecked)
                            {
                                employeeQuiz.isGraded = true;
                            }
                        }
                        employeeQuizes.Add(employeeQuiz);
                    }                 
                }
            }
            return Json(employeeQuizes, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetUserQuizes(string id)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, managers))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            Queries query = new Queries();
            User user = query.getUserById(Convert.ToInt32(id));
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
                        if ((bool)uqqa.isChecked)
                        {
                            employeeQuiz.isGraded = true;
                        }
                    }

                    employeeQuizes.Add(employeeQuiz);

                }
            }

            return Json(employeeQuizes, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmployeeQuizes(int id)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, managers))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            ViewBag.id = id;
            return View();
        }

        public ActionResult GetAllQuizes()
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            Queries query = new Queries();
            List<Quize> quizes = query.getUniqueQuizes();
            List<Quize> sending = new List<Quize>();
            foreach (Quize quiz in quizes)
            {
                Quize q = new Quize();
                q.id = quiz.id;
                q.title = quiz.title;
                q.description = quiz.description;
                q.startDate = quiz.startDate;
                q.expiredDate = quiz.expiredDate;
                sending.Add(q);
            }
            return Json(sending, JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult RemoveQuiz(string id)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }
            if (!val.checkUserAuth(vmUser, hrGroup))
            {
                return RedirectToAction("Index", "Home", new { error = "Invalid User Credentials" });
            }
            Queries query = new Queries();
            if (query.RemoveQuiz(Convert.ToInt32(id)))
            {
                return Json("success");
            }
            return Json("Failed");
        }

        public ActionResult TakeQuiz(string id)
        {
            UserQuizViewModel uqvm = new UserQuizViewModel();
            uqvm = GetQuizById(Convert.ToInt32(id));
            return Json(uqvm, JsonRequestBehavior.AllowGet);
        }

        public UserQuizViewModel GetQuizById(int id)
        {
            int quizId = Convert.ToInt32(id);
            Queries query = new Queries();
            Quize quiz = query.getQuizById(quizId);

            UserQuizViewModel uqvmQuiz = new UserQuizViewModel();

            uqvmQuiz.QuizId = quizId;
            uqvmQuiz.title = quiz.title;
            uqvmQuiz.description = quiz.description;
            uqvmQuiz.media = quiz.media;
            uqvmQuiz.startDate = quiz.startDate;
            uqvmQuiz.preferDate = quiz.preferDate;
            uqvmQuiz.expiredDate = quiz.expiredDate;
            uqvmQuiz.questions = new List<UserQuizVMQuestion>();
            
            List<Question> questions = query.getQuestionsByQuiz(quiz.id);

            foreach (Question question in questions)
            {
                UserQuizVMQuestion uqvmQuestion = new UserQuizVMQuestion();
                uqvmQuestion.id = question.id;
                uqvmQuestion.text = question.questionText;
                uqvmQuestion.type = question.questionType;
                uqvmQuestion.answers = new List<UserQuizVMAnswer>();
                List<Answer> answers = query.getAnswersByQuestion(question.id);

                foreach (Answer answer in answers)
                {
                    UserQuizVMAnswer uqvmAnswer = new UserQuizVMAnswer();
                    uqvmAnswer.id = answer.id;
                    uqvmAnswer.answerText = answer.answerText;
                    uqvmAnswer.isCorrect = answer.isCorrect;
                    uqvmQuestion.answers.Add(uqvmAnswer);
                }
                uqvmQuiz.questions.Add(uqvmQuestion);
            }
            return uqvmQuiz;
        }

        [HttpPost]
        public ActionResult SubmitQuiz(string answers)
        {
            UserViewModel vmUser = session.getSessionUser();
            if (vmUser == null)
            {
                if (!val.getUserCredentials(Request))
                {
                    return RedirectToAction("Login", "Home");
                }
                vmUser = session.getSessionUser();
            }

            Queries query = new Queries();
            User user = query.getUserBySam(vmUser.modelToUser(session.getSessionUser()).SAMAccountName);
            var result = JsonConvert.DeserializeObject<List<UserAnswer>>(answers);
            List<UserQuizQuestionAnswer> uqqas = new List<UserQuizQuestionAnswer>();
            foreach (UserAnswer answer in result)
            {              
                UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                uqqa.quizId = answer.quizId;
                uqqa.questionId = answer.questionId;
                uqqa.answerId = answer.answerId;
                uqqa.userId = user.id;
                uqqa.text = answer.answerText;               
                uqqas.Add(uqqa);               
            }
            List<UserQuizQuestionAnswer> addedUqqas = new List<UserQuizQuestionAnswer>();
            addedUqqas = query.submitQuiz(uqqas);
            if(addedUqqas.Count > 0)
            {
                return Json("Quiz Completed", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed to submit quiz", JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult RemoveQuestion(string ids)
        {
            try
            {
                var result = JsonConvert.DeserializeObject<JIds>(ids);
                Queries query = new Queries();
                
                foreach (int i in result.ids)
                {
                    query.RemoveQuestion(i);
                }
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch(Exception e)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
          
        }

        public ActionResult EditTraining(int id)
        {
            Queries query = new Queries();
            UserQuizViewModel uqvmQuiz = new UserQuizViewModel();
            Quize quiz = new Quize();
            quiz = query.getQuizById(id);
            uqvmQuiz.title = quiz.title;
            uqvmQuiz.description = quiz.description;
            uqvmQuiz.groups = new List<UserQuizGroup>();
            List<Quize> quizes = query.getQuizByTitle(query.getQuizById(id).title);
            foreach(Quize q in quizes)
            {
                UserQuizGroup group = new UserQuizGroup();
                group.id = q.groupId;
                group.name = query.getGroupById(group.id).name;
                uqvmQuiz.groups.Add(group);
            }
            return View(uqvmQuiz);
        }
    }
}