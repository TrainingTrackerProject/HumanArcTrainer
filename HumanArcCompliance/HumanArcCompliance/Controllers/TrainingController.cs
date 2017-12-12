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
        public ActionResult updateTraining(int id = 0)
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

        public ActionResult GradeQuiz(int userId = 0, int quizId = 0)
        {
            if (userId == 0 || quizId == 0)
            {
                RedirectToAction("ManageEmployees", "Training");
            }
            UserViewModel vmUser = session.getSessionUser();
            Queries query = new Queries();
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
            GradeViewModel gvmQuiz = GetGradedQuizById(userId, quizId);
            return View(gvmQuiz);
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
            List<Group> result = new List<Group>();
            foreach (Group group in groups)
            {
                Group g = new Group();
                g.id = group.id;
                g.name = group.name;
                result.Add(g);
            }
            return Json(result, JsonRequestBehavior.AllowGet);
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
            foreach (int groupId in result.groups)
            {
                Quize quiz = new Quize();
                quiz.groupId = Convert.ToInt32(groupId);
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
                int questionId;
                questionId = query.addQuestion(question);
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

        public ActionResult UpdateQuizQuestionAnswers(int[] questionIds, JQuestion questionData)
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
           
            foreach (int index in questionIds)
            {
                Question question = new Question();
                question.id = index;
                question.questionText = questionData.questionText;
                question.questionType = questionData.questionType;
                query.updateExistingQuestion(question);
                List<Answer> answers = query.getAnswersByQuestionId(index);
                if(questionData.questionType != "shortAnswer")
                {
                    for (int i = 0; i < questionData.answers.Count; i++)
                    {
                        answers[i].answerText = questionData.answers[i].answerText;
                        answers[i].isCorrect = questionData.answers[i].isCorrect;
                        query.updateExistingAnswer(answers[i]);
                    }
                }            
            }
            return Json(JsonRequestBehavior.AllowGet);
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
                        List<Question> questions = query.getQuestionsByQuizId(quiz.id);
                        if (questions.Count > 0)
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
                            //UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                            List<UserQuizQuestionAnswer> uqqas = query.getQuizByUserIdQuizId(user.id, quiz.id);
                            double uqqaCount = uqqas.Count();
                            if (uqqaCount > 0)
                            {
                                employeeQuiz.isCompleted = true;
                                employeeQuiz.isGraded = true;
                                double numberCorrect = 0;
                                foreach(UserQuizQuestionAnswer uqqa in uqqas)
                                {
                                    if ((bool)uqqa.isApproved)
                                    {
                                        numberCorrect++;
                                    }
                                    else if(query.getQuestionById(uqqa.questionId).questionType == "shortAnswer" && uqqa.isChecked == false)
                                    {
                                        uqqaCount--;
                                        employeeQuiz.isGraded = false;
                                    }
                                }
                                employeeQuiz.percentCorrect = numberCorrect / uqqaCount;
                            }                           
                            employeeQuizes.Add(employeeQuiz);
                        }                       
                    }                 
                }
            }
            return Json(employeeQuizes, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult GetUserQuizes(int id)
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
            User user = query.getUserById(id);
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
                    List<UserQuizQuestionAnswer> uqqas = query.getQuizByUserIdQuizId(user.id, quiz.id);
                    if (uqqas.Count > 0)
                    {
                        uqqa = uqqas[0];
                        if (uqqa.id != 0)
                        {
                            employeeQuiz.isCompleted = true;
                            if (uqqa.isChecked == true)
                            {
                                employeeQuiz.isGraded = true;
                                if (uqqa.isChecked == true)
                                {
                                    employeeQuiz.isGraded = true;
                                }
                                else
                                {
                                    employeeQuiz.isGraded = false;
                                }
                            }
                            else
                            {
                                employeeQuiz.isGraded = false;
                            }
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
            List<Quize> results = new List<Quize>();
            foreach (Quize quiz in quizes)
            {
                Quize q = new Quize();
                q.id = quiz.id;
                q.title = quiz.title;
                q.description = quiz.description;
                q.startDate = quiz.startDate;
                q.expiredDate = quiz.expiredDate;
                results.Add(q);
            }
            return Json(results, JsonRequestBehavior.AllowGet);
        }
        public GradeViewModel GetGradedQuizById(int uId, int qId)
        {
            int userId = Convert.ToInt32(uId);
            int quizId = Convert.ToInt32(qId);
            Queries query = new Queries();
            Quize quiz = query.getQuizById(quizId);

            GradeViewModel gvmQuiz = new GradeViewModel();

            gvmQuiz.QuizId = quizId;
            gvmQuiz.title = quiz.title;
            gvmQuiz.description = quiz.description;
            gvmQuiz.media = quiz.media;
            gvmQuiz.questions = new List<GradeVMQuestion>();

            List<Question> questions = query.getQuestionsByQuizId(quiz.id);

            foreach (Question question in questions)
            {
                GradeVMQuestion gvmQuestion = new GradeVMQuestion();
                gvmQuestion.id = question.id;
                gvmQuestion.text = question.questionText;
                gvmQuestion.type = question.questionType;
                Answer answer = query.getUserAnswerByQuestionId(userId, question.id);

                gvmQuestion.answerId = answer.id;
                gvmQuestion.answerText = answer.answerText;

                gvmQuiz.questions.Add(gvmQuestion);
            }
            return gvmQuiz;
        }
        public ActionResult RemoveQuiz(int id)
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
            if (query.RemoveQuiz(id))
            {
                return Json("success");
            }
            return Json("Failed");
        }

        public ActionResult ViewQuiz(int id)
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
            UserQuizViewModel uqvm = new UserQuizViewModel();
            uqvm = GetQuizById(id);
            Queries query = new Queries();
            List<UserQuizQuestionAnswer> uqqas = query.getQuizByUserIdQuizId(query.getUserBySam(vmUser.SAMAccountName).id, id);

            if (uqqas.Count > 0)
            {
                uqvm.isTaken = true;
                ViewModelConverter vmConverter = new ViewModelConverter();
                uqvm.juqqas = new List<JUserQuizQuestionAnswer>();
                foreach (UserQuizQuestionAnswer uqqa in uqqas)
                {
                    uqvm.juqqas.Add(vmConverter.UserQuizQuestionAnswerToJModel(uqqa));
                }
            }
            return Json(uqvm, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ViewGradeQuiz(int id)
        {
            UserViewModel vmUser = session.getSessionUser();
            Queries query = new Queries();
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
            GradeViewModel gvm = new GradeViewModel();
            User user = query.getUserBySam(vmUser.modelToUser(vmUser).SAMAccountName);
            gvm = GetGradedQuizById(user.id, id);
            List<UserQuizQuestionAnswer> uqqas = query.getQuizByUserIdQuizId(user.id, id);
            
            if (uqqas.Count > 0)
            {
                ViewModelConverter vmConverter = new ViewModelConverter();
                gvm.juqqas = new List<JUserQuizQuestionAnswer>();
                foreach (UserQuizQuestionAnswer uqqa in uqqas)
                {
                    gvm.juqqas.Add(vmConverter.UserQuizQuestionAnswerToJModel(uqqa));
                }
            }
            return Json(gvm, JsonRequestBehavior.AllowGet);
        }

        public UserQuizViewModel GetQuizById(int quizId)
        {
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
            
            List<Question> questions = query.getQuestionsByQuizId(quiz.id);

            foreach (Question question in questions)
            {
                UserQuizVMQuestion uqvmQuestion = new UserQuizVMQuestion();
                uqvmQuestion.id = question.id;
                uqvmQuestion.text = question.questionText;
                uqvmQuestion.type = question.questionType;
                uqvmQuestion.answers = new List<UserQuizVMAnswer>();
                List<Answer> answers = query.getAnswersByQuestionId(question.id);

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
        public ActionResult SubmitQuiz(UserAnswer[] answers)
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
            //var result = JsonConvert.DeserializeObject<List<UserAnswer>>(answers);
            List<UserQuizQuestionAnswer> uqqas = new List<UserQuizQuestionAnswer>();
            foreach (UserAnswer answer in answers)
            {              
                UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                uqqa.quizId = answer.quizId;
                uqqa.questionId = answer.questionId;
                uqqa.answerId = answer.answerId;
                uqqa.userId = user.id;
                uqqa.text = answer.answerText;
                uqqas.Add(uqqa);
            }
            uqqas = GradeQuiz(GetQuizById(answers[0].quizId), uqqas);
            List<UserQuizQuestionAnswer> addedUqqas = new List<UserQuizQuestionAnswer>();
            addedUqqas = query.submitQuiz(uqqas);
            if(addedUqqas.Count > 0)
            {
                return Json("Quiz Completed", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed to submit quiz", JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult SubmitGrade(UserAnswer[] answers)
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
            //var result = JsonConvert.DeserializeObject<List<UserAnswer>>(answers);
            List<UserQuizQuestionAnswer> uqqas = new List<UserQuizQuestionAnswer>();
            foreach (UserAnswer answer in answers)
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
            if (addedUqqas.Count > 0)
            {
                return Json("Quiz Completed", JsonRequestBehavior.AllowGet);
            }
            return Json("Failed to submit quiz", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveQuestion(int[] ids)
        {
            try
            {
                //var result = JsonConvert.DeserializeObject<JIds>(ids);
                Queries query = new Queries();
                
                foreach (int i in ids)
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
            UserQuizViewModel uqvmQuiz = GetQuizById(id);
            return Json(uqvmQuiz, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetQuestionAnswers(string questionId)
        {
            Queries query = new Queries();
            Question question = query.getQuestionById(Convert.ToInt32(questionId));
            List<Answer> answers = query.getAnswersByQuestionId(question.id);
            List<JAnswers> jAnswers = new List<JAnswers>();
            foreach(Answer ans in answers)
            {
                JAnswers jAnswer = new JAnswers();
                jAnswer.answerText = ans.answerText;
                jAnswer.isCorrect = ans.isCorrect;
                jAnswers.Add(jAnswer);
            }
            JQuestion jQuestion = new JQuestion();
            jQuestion.id = questionId;
            jQuestion.questionText = question.questionText;
            jQuestion.questionType = question.questionType;
            jQuestion.answers = jAnswers;

            return Json(jQuestion, JsonRequestBehavior.AllowGet);
        }

        public List<UserQuizQuestionAnswer> GradeQuiz(UserQuizViewModel quiz, List<UserQuizQuestionAnswer> uqqas)
        {
            int questionCount = quiz.questions.Count();
            foreach(UserQuizVMQuestion vmQuestion in quiz.questions)
            {
                if(vmQuestion.type != "shortAnswer")
                {
                    for (var i = 0; i < questionCount; i++)
                    {
                        if (vmQuestion.answers[i].id == uqqas[i].answerId)
                        {
                            uqqas[i].isApproved = true;
                        }
                    }
                }
                
            }
            return uqqas;
        }
    }
}