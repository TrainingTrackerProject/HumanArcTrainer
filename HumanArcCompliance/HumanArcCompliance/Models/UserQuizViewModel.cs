using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class UserQuizViewModel
    {
        public bool isHR { get; set; }
        public bool isManager { get; set; }
        public int QuizId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string media { get; set; }
        public bool isTaken { get; set; }
        public DateTime startDate { get; set; }
        public DateTime preferDate { get; set; }
        public DateTime expiredDate { get; set; }
        public List<UserQuizVMQuestion> questions { get; set; }
        public List<UserQuizGroup> groups { get; set; }
    }

    public class UserQuizVMQuestion
    {
        public int id { get; set; }
        public string text { get; set; }
        public string type { get; set; }
        public List<UserQuizVMAnswer> answers { get; set; }
    }

    public class UserQuizVMAnswer
    {
        public int id { get; set; }
        public string answerText { get; set; }
        public bool isCorrect { get; set; }
    }

    public class UserQuizGroup
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}