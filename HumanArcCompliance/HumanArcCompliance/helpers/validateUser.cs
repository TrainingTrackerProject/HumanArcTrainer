using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.DirectoryServices;
using HumanArcCompliance.Models;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using HumanArcCompliance.helpers;

namespace HumanArcCompliance
{
    public class validateUser
    {
        // not used in actual application this is for test purposes where there is no access to active directory
        public ADUser validate(){
            ADUser myADUser = new ADUser();
            sessionStorage session = new sessionStorage();
            string conn = ConfigurationManager.ConnectionStrings["HumanArcEntities"].ConnectionString;
            using (SqlConnection connection = new SqlConnection(conn))
            {
                connection.Open();
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;

                //for HR Rep
                cmd.CommandText = "SELECT * FROM Users where SAMAccountName = 'Administrator' ";

                //for management
                //cmd.CommandText = "SELECT * FROM Users where SAMAccountName = 'managerSAM'";

                //for normal user
                //cmd.CommandText = "SELECT * FROM Users where SAMAccountName = 'userSAM'";

                cmd.CommandType = CommandType.Text;
                cmd.Connection = connection;



                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();

                    myADUser.givenName = (string)reader["firstName"];
                    myADUser.sn = (string)reader["lastName"];
                    myADUser.sAMAccountName = (string)reader["SAMAccountName"];
                    myADUser.mail = (string)reader["email"];
                    string groups = (string)reader["userGroups"];
                    myADUser.memberOf = groups.Split(',');
                }
                if (myADUser.sAMAccountName == "Administrator")
                {
                    myADUser.isHR = "true";
                    myADUser.isManager = "false";
                }
                else if (myADUser.sAMAccountName == "managerSAM")
                {
                    myADUser.isHR = "false";
                    myADUser.isManager = "true";
                }
                else
                {
                    myADUser.isHR = "false";
                    myADUser.isManager = "false";
                }

                connection.Close();
            }
            session.setSessionVars(myADUser);
            return myADUser;
        }
        
    }
}