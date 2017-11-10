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

        public Quize getQuiz(int id)
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
    }
}
