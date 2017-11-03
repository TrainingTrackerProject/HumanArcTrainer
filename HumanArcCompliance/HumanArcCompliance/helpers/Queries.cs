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
        public void checkExistingUser(ADUser myADUser)
        {

<<<<<<< HEAD
            //using (var ctx = new HumanArcEntities())
            //{
            //    var DBUser = ctx.Users.Where(Users => Users.SAMAccountName == myADUser.sAMAccountName);

            //}
            return true;
=======

            string conn = ConfigurationManager.ConnectionStrings["HumanArcEntities"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader searchReader;

                cmd.CommandText = "SELECT * FROM Users where SAMAccountName = '"+myADUser.sAMAccountName+"'";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;

                searchReader = cmd.ExecuteReader();
                if (!searchReader.HasRows)
                {
                    searchReader.Close();
                    SqlDataReader insertReader;

                    cmd.CommandText = "insert into Users (firstName, lastName, email, userGroups, SAMAccountName)" +
                        "Values('" + myADUser.givenName + "','" + myADUser.sn + "','" + myADUser.mail + "','" +                        
                        myADUser.memberOf.ToString() + "','" + myADUser.sAMAccountName + "');";
                    insertReader = cmd.ExecuteReader();
                }   
                
                connection.Close();
            }
>>>>>>> 047c5b76838955201fd965ff4d476d3464ae2c00
        }
    }
}
