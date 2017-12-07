using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanArcCompliance.Models;
using System.Data.Entity;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace HumanArcCompliance.helpers
{
    public class Queries
    {
      

        string conn = ConfigurationManager.ConnectionStrings["HumanArcEntities"].ConnectionString;

        public void checkExistingUser(User myUser)
        {

            HumanArcEntities db = new HumanArcEntities();
            User thisUser = db.Users.First(user => user.SAMAccountName == myUser.SAMAccountName);
            if(thisUser != null)
            {
                if(myUser.firstName != thisUser.firstName || myUser.lastName != thisUser.lastName || myUser.email != thisUser.email ||myUser.userGroups != thisUser.userGroups || myUser.manager != thisUser.manager)
                {
                    thisUser = myUser;
                }
            }
            else
            {
                db.Users.Add(myUser);
            }
            db.SaveChanges();

        }
    

        public List<User> getAllUsers()
        {
            HumanArcEntities db = new HumanArcEntities();
            List<User> users = db.Users.ToList();     
            return users;
        }

        public List<Group> getAllGroups()
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Groups.ToList();
        }

        public Quize getQuizById(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Quizes.Find(id);
        }
        public UserQuizQuestionAnswer getAnswerToShortAnswerQuestion(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.UserQuizQuestionAnswers.First(a => a.questionId == id);
        }
        public int addQuiz(Quize q)
        {          
            HumanArcEntities db = new HumanArcEntities();
            db.Quizes.Add(q);
            db.SaveChanges();
            return q.id;           
        }

        public Question getQuestion(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Questions.Find(id);
        }

        public int addQuestion(Question q)
        {
            HumanArcEntities db = new HumanArcEntities();
            db.Questions.Add(q);
            db.SaveChanges();
            return q.id;
        }

        public Answer getAnswer(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Answers.Find(id);
        }

        public int addAnswer(Answer a)
        {          
            HumanArcEntities db = new HumanArcEntities();
            db.Answers.Add(a);
            db.SaveChanges();
            return a.id;         
        }

        public List<UserQuizQuestionAnswer> getAllUserQuizes(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            List<UserQuizQuestionAnswer> userQuizes = new List<UserQuizQuestionAnswer>();
            userQuizes = db.UserQuizQuestionAnswers.Where(quiz => quiz.userId == id).ToList();
            return userQuizes;
        }

        public List<Question> getQuestionsByQuiz(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Questions.Where(q => q.quizId == id).ToList();
        }

        public List<Answer> getAnswersByQuestion(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Answers.Where(a => a.questionId == id).ToList();
        }

        public User getUserById(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Users.First(u => u.id == id);
        }

        public List<Quize> getQuizesByGroupId(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Quizes.Where(q => q.groupId == id).ToList();
        }
        public Group getGroupByName(string name)
        {
            HumanArcEntities db = new HumanArcEntities();
            try
            {
                return db.Groups.First(g => g.name == name);
            }
            catch (Exception e)
            {
                Group group = new Group();
                return group;
            }
            
        }

        public UserQuizQuestionAnswer getQuizByUserIdQuizId(int userId, int quizId)
        {
            HumanArcEntities db = new HumanArcEntities();
            try
            {
                UserQuizQuestionAnswer uqqa = db.UserQuizQuestionAnswers.First(uq => uq.userId == userId && uq.quizId == quizId);
                if(uqqa.isChecked == null)
                {
                    uqqa.isChecked = false;
                }
                if (uqqa.isApproved == null)
                {
                    uqqa.isApproved = false;
                }
                return uqqa;
            }
            catch (Exception e)
            {
                UserQuizQuestionAnswer uqqa = new UserQuizQuestionAnswer();
                return uqqa;
            }
        }
        
        public List<Quize> getAllQuizes()
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Quizes.ToList();
        }

        public List<UserQuizQuestionAnswer> submitQuiz(List<UserQuizQuestionAnswer> answers)
        {
            HumanArcEntities db = new HumanArcEntities();
            List<UserQuizQuestionAnswer> uqqas = new List<UserQuizQuestionAnswer>();
            try
            {
                foreach (UserQuizQuestionAnswer uqqa in answers)
                {
                    uqqa.isChecked = false;
                    uqqa.isApproved = false;
                    db.UserQuizQuestionAnswers.Add(uqqa);
                    db.SaveChanges();
                    uqqas.Add(uqqa);
                }
                return uqqas;
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return uqqas;
            }
        }

        public User getUserBySam(string sam)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Users.First(u => u.SAMAccountName == sam);
        }

        public bool RemoveQuiz(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            Quize chosenQuiz = db.Quizes.First(q => q.id == id);
            List<Quize> quizes = db.Quizes.Where(q => q.title == chosenQuiz.title).ToList();
            foreach(Quize quiz in quizes)
            {
                List<Question> Questions = db.Questions.Where(quest => quest.quizId == quiz.id).ToList();
                try
                {
                    foreach (Question question in Questions)
                    {
                        List<Answer> Answers = db.Answers.Where(ans => ans.questionId == question.id).ToList();
                        foreach (Answer answer in Answers)
                        {
                            List<UserQuizQuestionAnswer> UserQuizzes = db.UserQuizQuestionAnswers.Where(uqqa => uqqa.answerId == answer.id).ToList();
                            foreach (UserQuizQuestionAnswer uqqa in UserQuizzes)
                            {
                                db.UserQuizQuestionAnswers.Remove(db.UserQuizQuestionAnswers.First(u => u.id == uqqa.id));
                                db.SaveChanges();
                            }
                            db.Answers.Remove(db.Answers.First(ans => ans.id == answer.id));
                            db.SaveChanges();
                        }
                        db.Questions.Remove(db.Questions.First(quest => quest.id == question.id));
                        db.SaveChanges();
                    }
                    db.Quizes.Remove(db.Quizes.First(q => q.id == quiz.id));
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// This methods removes a question and all associated answers. There is no check for UQQAs because this can only be done before the start date of the quiz.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool RemoveQuestion(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            List<Answer> Answers = db.Answers.Where(ans => ans.questionId == id).ToList();
            try
            {
                foreach (Answer answer in Answers)
                {
                    db.Answers.Remove(db.Answers.First(ans => ans.id == answer.id));
                }
                db.Questions.Remove(db.Questions.First(quest => quest.id == id));
                db.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
       }

        public Group getGroupById(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Groups.First(g => g.id == id);
        }

        public List<Quize> getQuizByTitle(string title)
        {
            HumanArcEntities db = new HumanArcEntities();
            Quize quiz = new Quize();
            try
            {
                List<Quize> quizes = db.Quizes.Where(q => q.title == title).ToList();
                return quizes;
            }
            catch(Exception e)
            {
                List<Quize> quizes = new List<Quize>();
                return quizes;
            }
        }

        public List<Quize> getUniqueQuizes()
        {
            HumanArcEntities db = new HumanArcEntities();
            List<Quize> returnedList = new List<Quize>();
            var result = db.Quizes.GroupBy(q => q.title).ToList();
            foreach (var list in db.Quizes.GroupBy(q => q.title).ToList())
            {
                var quizList = list.ToList();
                returnedList.Add((Quize)quizList[0]);
            }
            return returnedList;
        }

        public bool updateExistingQuiz(Quize updatedQuiz)
        {
            try
            {
                Quize quiz = new Quize();
                using (var ctx = new HumanArcEntities())
                {
                    quiz = ctx.Quizes.First(q => q.id == updatedQuiz.id);
                }

                quiz.description = updatedQuiz.description;
                quiz.media = updatedQuiz.media;
                quiz.startDate = updatedQuiz.startDate;
                quiz.preferDate = updatedQuiz.preferDate;
                quiz.expiredDate = updatedQuiz.expiredDate;

                //save modified entity using new Context
                using (var dbCtx = new HumanArcEntities())
                {
                    dbCtx.Entry(quiz).State = System.Data.Entity.EntityState.Modified;

                    dbCtx.SaveChanges();
                }
                //HumanArcEntities db = new HumanArcEntities();
                //db.Quizes.Attach(updatedQuiz);
                //var entry = db.Entry(updatedQuiz);

                //entry.Property(e => e.description).IsModified = true;
                //entry.Property(e => e.startDate).IsModified = true;
                //entry.Property(e => e.preferDate).IsModified = true;
                //entry.Property(e => e.expiredDate).IsModified = true;
                //db.SaveChanges();
            }
            catch(Exception e)
            {
                return false;
            }
            return true;
        }
    }
}
