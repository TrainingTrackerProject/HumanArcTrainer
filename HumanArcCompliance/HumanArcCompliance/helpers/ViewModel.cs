using HumanArcCompliance.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.helpers
{
    public class ViewModel
    {
        public UserViewModel userToModel(User myUser)
        {
            validateUser validate = new validateUser();
            UserViewModel vmUser = new UserViewModel();
            vmUser.firstName = myUser.firstName;
            vmUser.lastName = myUser.lastName;
            vmUser.email = myUser.email;
            vmUser.SAMAccountName = myUser.SAMAccountName;
            vmUser.manager = myUser.manager;
            vmUser.userGroups = myUser.userGroups.Split(',');
            vmUser.isHR = "false";
            vmUser.isManager = "false";
            if (validate.validateHR(vmUser.userGroups))
            {
                vmUser.isHR = "true";
            }
            else
            {
                if (validate.validateManager(vmUser.userGroups))
                {
                    vmUser.isManager = "true";
                }
            }

            return vmUser;
        }

        public User modelToUser(UserViewModel vmUser)
        {
            User myUser = new User();
            myUser.firstName = vmUser.firstName;
            myUser.lastName = vmUser.lastName;
            myUser.email = vmUser.email;
            myUser.SAMAccountName = vmUser.SAMAccountName;
            myUser.manager = vmUser.manager;
            myUser.userGroups = vmUser.userGroups.ToString();
            return myUser;
        }
    }
}