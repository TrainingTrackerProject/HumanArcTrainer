using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.helpers
{
    public class deserialize
    {

        public class Answers
        {
            public string answerText { get; set; }
            public bool isCorrect { get; set; }
        }

        public class Questions
        {
            public string type { get; set; }
            public string text { get; set; }
            public Answers[] answers { get; set; }
        }

        public class Quiz
        {
            public string title { get; set; }
            public string description { get; set; }
            public Questions[] questions { get; set; }


        }

        public class RootObject
        {
            public List<Quiz> result { get; set; }
        }
    }
}