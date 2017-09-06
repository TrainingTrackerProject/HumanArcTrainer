using System.Collections.Generic;

namespace HumanArc.Compliance.Shared.Entities
{
    public class Question : EntityBase
    {
        public string Text { get; set; }
        public virtual ICollection<Answer> Answers { get; set; } 
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
    }
}
