using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    //used to parse information
    public class AddQuizDeserializer
    {
        public class JAnswers
        {
            public string answerText { get; set; }
            public string isCorrect { get; set; }
        }

        public class JQuestions
        {
            public string type { get; set; }
            public string text { get; set; }
            public List<JAnswers> answers { get; set; }
        }

        public class JQuiz
        {
            public string title { get; set; }
            public string description { get; set; }
            public List<JQuestions> questions { get; set; }
        }
    }
}