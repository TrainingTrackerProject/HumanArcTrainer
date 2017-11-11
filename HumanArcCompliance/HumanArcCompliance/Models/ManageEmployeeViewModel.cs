using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class ManageEmployeeViewModel
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string SAMAccountName { get; set; }
        public string manager { get; set; }
        public bool hasUngradedQuiz { get; set; }
    }
}