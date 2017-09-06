

using System.Collections.Concurrent;

namespace HumanArc.Compliance.Shared.ViewModels
{
   public class AnswerViewModel
    {
        public int? ID { get; set; }
        public string Text { get; set; }
        public int SortOrder { get; set; }
        public int QuestionId { get; set; }
    }
}
