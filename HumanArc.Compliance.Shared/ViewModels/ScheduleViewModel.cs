using HumanArc.Compliance.Shared.Enumerations;
using System;
using System.Collections.Generic;

namespace HumanArc.Compliance.Shared.ViewModels
{
    public class ScheduleViewModel
    {
        public int ID { get; set; }
        public int TrainingID { get; set; }
        public EmailFrequencyEnum EmailFrequency { get; set; }
        public DateTime StartDay { get; set; }
        public DateTime EndDay { get; set; }
        public TrainingFrequencyEnum TrainingFrequency { get; set; }
       
    }
}
