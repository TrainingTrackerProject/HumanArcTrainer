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
            return db.Questions.Where(q => q.id == id).ToList();
        }

        public List<Answer> getAnswersByQuestion(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Answers.Where(a => a.id == id).ToList();
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
                return db.UserQuizQuestionAnswers.First(uq => uq.userId == userId && uq.quizId == quizId);
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

        public void RemoveQuiz(int id)
        {
            HumanArcEntities db = new HumanArcEntities();
            List<Question> Questions = db.Questions.Where(q => q.quizId == id).ToList();
            foreach (Question question in Questions)
            {

                List<Answer> Answers = db.Answers.Where(q => q.questionId == question.id).ToList();
                foreach (Answer answer in Answers)
                {
                  
                    List<UserQuizQuestionAnswer> UserQuizzes = db.UserQuizQuestionAnswers.Where(q => q.answerId == answer.id).ToList();
                    foreach (UserQuizQuestionAnswer uqqa in UserQuizzes)
                    {
                        db.UserQuizQuestionAnswers.Remove(db.UserQuizQuestionAnswers.First(u => u.id == uqqa.id));
                        db.SaveChanges();
                    }
                    db.Answers.Remove(db.Answers.First(a => a.id == answer.id));
                    db.SaveChanges();

                }
                db.Questions.Remove(db.Questions.First(q => q.id == question.id));
                db.SaveChanges();
            }
            db.Quizes.Remove(db.Quizes.First(q => q.id == id));
            db.SaveChanges();
        }

    }
}
