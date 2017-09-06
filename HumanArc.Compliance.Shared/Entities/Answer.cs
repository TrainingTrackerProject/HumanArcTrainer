using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanArc.Compliance.Shared.Entities
{
    public class Answer : EntityBase
    {
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public int QuestionId { get; set; }
    }
}
