using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class EmployeeQuizesViewModel
    {
        public int userId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int quizId { get; set; }
        public string quizTitle { get; set; }
        public bool isCompleted { get; set; }
        public bool isGraded { get; set; }
    }
}