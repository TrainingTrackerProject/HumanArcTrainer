using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    //Active Driectory User
    public class UserViewModel
    {
        // First Name
        public String firstName { get; set; }
        // Last Name
        public String lastName { get; set; }
        //email
        public String email { get; set; }

        public String isHR { get; set; }

        public String isManager { get; set; }

        public String SAMAccountName { get; set; }

        public String[] userGroups { get; set; }

        public String manager { get; set; }
    }

}