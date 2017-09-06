using System.Collections.Generic;


namespace HumanArc.Compliance.Shared.ViewModels
{
    public class TrainingViewModel
    {
        public int? ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string FilePathToMedia { get; set; }
        public IList<QuestionViewModel> Questions { get; set; }
        public ScheduleViewModel Schedule { get; set; }
        public bool HasTrainingFile { get; set; }
        public List<ADGroupViewModel> ADGroups { get; set; }
    }
}
