using HumanArc.Compliance.Shared.Enumerations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanArc.Compliance.Shared.Entities
{
    public class ADGroup : EntityBase
    {
        public string GroupName { get; set; }
        public virtual IList<Training> Training { get; set; }
    }
}
