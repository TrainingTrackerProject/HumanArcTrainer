using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class SubmitQuizDeserializer
    {
        public class UserAnswer
        {
            public int quizId { get; set; }
            public int questionId { get; set; }
            public int answerId { get; set; }
            public string answerText { get; set; }
        }       
    }
}