using System;

namespace HumanArc.Compliance.Shared.Entities
{
    public class EntityBase
    {
        public int ID { get; set; }
        public DateTime? DateCreated { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? DateModified { get; set; }
        public string ModifiedBy { get; set; }
        public bool Deleted { get; set; }
    }
}
