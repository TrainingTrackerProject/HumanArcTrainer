using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    //Active Driectory User
    public class ADUser
    {
        // First Name
        public String givenName { get; set; }
        // Last Name
        public String sn { get; set; }
        //email
        public String mail { get; set; }

        public String isHR { get; set; }

        public String isManager { get; set; }

        public String sAMAccountName { get; set; }
    }
}