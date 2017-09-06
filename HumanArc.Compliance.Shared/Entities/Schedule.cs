using HumanArc.Compliance.Shared.Enumerations;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HumanArc.Compliance.Shared.Entities
{
    public class Schedule : EntityBase
    {     
        public EmailFrequencyEnum EmailFrequency { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public TrainingFrequencyEnum TrainingFrequency { get; set; }

        [Key, ForeignKey("Training")]
        public int TrainingId { get; set; }
        public virtual Training Training { get; set; }
    }
}
