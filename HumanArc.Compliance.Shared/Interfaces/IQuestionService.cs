

using System.Collections.Generic;
using HumanArc.Compliance.Shared.ViewModels;

namespace HumanArc.Compliance.Shared.Interfaces
{
    public interface IQuestionService
    {
        QuestionViewModel GetById(int id);
        QuestionViewModel Save(QuestionViewModel model);
        List<QuestionViewModel> GetQuestionsByTrainingId(int id);
        bool Delete(int id);
    }
}
