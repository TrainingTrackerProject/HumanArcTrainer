using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class SubmitQuizDeserializer
    {
        public class SubmitAnswers
        {
            public int id { get; set; }
            public string text { get; set; }
        }

        public class SubmitQuestions
        {
            public int id { get; set; }
            public List<SubmitAnswers> answers { get; set; }
        }

        public class SubmitQuiz
        {
            public int id { get; set; }
            public List<SubmitQuestions> questions { get; set; }
        }
    }
}