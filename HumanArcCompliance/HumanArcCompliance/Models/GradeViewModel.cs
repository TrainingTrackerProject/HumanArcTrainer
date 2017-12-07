﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class GradeViewModel
    {
        public int QuizId { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string media { get; set; }
        public List<GradeVMQuestion> questions { get; set; }
    }

    public class GradeVMQuestion
    {
        public int id { get; set; }
        public string text { get; set; }
        public string type { get; set; }

        public int answerId { get; set; }
        public string answerText { get; set; }
    }
}