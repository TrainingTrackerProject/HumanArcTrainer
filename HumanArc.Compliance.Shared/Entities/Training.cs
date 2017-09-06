using System;
using System.Collections.Generic;

namespace HumanArc.Compliance.Shared.Entities
{
    public class Training : EntityBase
    {
        public string Name { get; set; }   
        public string Description { get; set; }     
        public string FilePathToMedia { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
        public virtual Schedule Schedule { get; set; }
        //public virtual IList<ADGroup> ADGroup { get; set; }
    }
}
