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

            //using (SqlConnection connection = new SqlConnection(conn))
            //{
            //    connection.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    SqlDataReader searchReader;

            //    cmd.CommandText = "SELECT * FROM Users where SAMAccountName = '"+myUser.SAMAccountName+"';";

            //    cmd.CommandType = CommandType.Text;
            //    cmd.Connection = connection;

            //    searchReader = cmd.ExecuteReader();
            //    if (!searchReader.HasRows)
            //    {
            //        searchReader.Close();
            //        SqlDataReader insertReader;

            //        cmd.CommandText = "insert into Users (firstName, lastName, email, userGroups, SAMAccountName, manager)" +
            //            "Values('" + myADUser.givenName + "','" + myADUser.sn + "','" + myADUser.mail + "','" +                        
            //            myADUser.memberOf.ToString() + "','" + myADUser.sAMAccountName + "','" + myADUser.manager + "');";
            //        insertReader = cmd.ExecuteReader();
            //    }
            //    else
            //    {
            //        searchReader.Read();
            //        string obj = (string)searchReader.GetValue(searchReader.GetOrdinal("manager"));
            //        string test = (string)searchReader.GetValue(searchReader.GetOrdinal("firstName"));
            //        bool isChanged = false;
            //        ADUser updateUser = new ADUser();
            //        if(myADUser.givenName != (string)searchReader.GetValue(searchReader.GetOrdinal("firstName")))
            //        {
            //            isChanged = true;
            //        }
            //        if (myADUser.sn != (string)searchReader.GetValue(searchReader.GetOrdinal("lastName")))
            //        {
            //            isChanged = true;
            //        }
            //        if (myADUser.mail != (string)searchReader.GetValue(searchReader.GetOrdinal("email")))
            //        {
            //            isChanged = true;
            //        }
            //        if (myADUser.manager != (string)searchReader.GetValue(searchReader.GetOrdinal("manager")))
            //        {
            //            isChanged = true;
            //        }
            //        if (isChanged)
            //        {
            //            searchReader.Close();
            //            cmd.CommandText = "UPDATE Users SET firstName = '" + myADUser.givenName + "', " +
            //                "lastName = '" + myADUser.sn + "', email = '" + myADUser.mail + "'," +
            //                "SAMAccountName = '" + myADUser.sAMAccountName + "', manager = '" + myADUser.manager + "' " +
            //                " WHERE SAMAccountName = '" + myADUser.sAMAccountName + "';";

            //            SqlDataReader updateReader;
            //            updateReader = cmd.ExecuteReader();
            //        }
            //    }               
            //    connection.Close();
        }
    

        public List<User> getAllUsers()
        {
            HumanArcEntities db = new HumanArcEntities();
            List<User> users = db.Users.ToList();

            //List<ADUser> userList = new List<ADUser>();
            //using (SqlConnection connection = new SqlConnection(conn))
            //{
            //    connection.Open();
            //    SqlCommand cmd = new SqlCommand();
            //    SqlDataReader searchReader;

            //    cmd.CommandText = "SELECT * FROM Users;";

            //    cmd.CommandType = CommandType.Text;
            //    cmd.Connection = connection;

            //    searchReader = cmd.ExecuteReader();
            //    while (searchReader.Read())
            //    {
            //        ADUser managersUser = new ADUser();
            //        managersUser.givenName = (string)searchReader.GetValue(searchReader.GetOrdinal("firstName"));
            //        managersUser.sn = (string)searchReader.GetValue(searchReader.GetOrdinal("lastName"));
            //        managersUser.mail = (string)searchReader.GetValue(searchReader.GetOrdinal("email"));
            //        managersUser.sAMAccountName = (string)searchReader.GetValue(searchReader.GetOrdinal("SAMAccountName"));
            //        managersUser.manager = (string)searchReader.GetValue(searchReader.GetOrdinal("manager"));
            //        userList.Add(managersUser);
            //    }
            //}
            return users;
        }

        public List<Group> getAllGroups()
        {
            HumanArcEntities db = new HumanArcEntities();
            return db.Groups.ToList();
        }
    }
}
