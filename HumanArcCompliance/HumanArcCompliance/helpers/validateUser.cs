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
        public User validate(){
            HumanArcEntities db = new HumanArcEntities();
            User myUser = new User();
            sessionStorage session = new sessionStorage();
            myUser = db.Users.First(User => User.SAMAccountName == "Administrator");
            return myUser;
        }

        public bool validateHR(string[] userGroups)
        {
            foreach(string group in userGroups)
            {
                if(group == "HRGroup")
                {
                    return true;
                }
            }
            return false;
        }

        public bool validateManager(string[] userGroups)
        {
            foreach (string group in userGroups)
            {
                if (group == "managers")
                {
                    return true;
                }
            }
            return false;
        }
    }
}