﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    //used to parse information
    public class Deserializers
    {
        public class JAnswers
        {
            public int id { get; set; }
            public string answerText { get; set; }
            public bool isCorrect { get; set; }
        }

        public class JQuestion
        {
            public List<int> quizIds { get; set; }
            public string id { get; set; }
            public string questionType { get; set; }
            public string questionText { get; set; } 
            public List<JAnswers> answers { get; set; }
        }

        public class JQuiz
        {
            public string title { get; set; }
            public string description { get; set; }
            public int[] groups { get; set; }
            public string media { get; set; }
            public System.DateTime startDate { get; set; }
            public System.DateTime preferredDate { get; set; }
            public System.DateTime expirationDate { get; set; }

        }

        public class JIds
        {
            public int[] ids { get; set; }
        }
    }
}