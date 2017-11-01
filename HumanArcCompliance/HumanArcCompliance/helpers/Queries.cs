using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using HumanArcCompliance.Models;
using System.Data.Entity;
namespace HumanArcCompliance.helpers
{
    public class Queries
    {
        public bool checkExistingUser(ADUser myADUser)
        {

            //using (var ctx = new HumanArcEntities())
            //{
            //    var DBUser = ctx.Users.Where(Users => Users.SAMAccountName == myADUser.sAMAccountName);

            //}
            return true;
        }
    }
}