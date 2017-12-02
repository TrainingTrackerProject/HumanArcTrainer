using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{

    //View Model
    //used to send specific quiz info to user taking quiz
    public class QuizViewModel
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public List<QVMQuestion> questions { get; set; }
    }

    public class QVMQuestion
    {
        public int id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
    }

}