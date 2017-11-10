using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class addQuiz
    {
        public string[] groups { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string media { get; set; }
        public string[] questions { get; set; }

    }
}