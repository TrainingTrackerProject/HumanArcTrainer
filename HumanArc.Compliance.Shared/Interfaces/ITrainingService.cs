using HumanArc.Compliance.Shared.Entities;
using HumanArc.Compliance.Shared.ViewModels;
using System.Collections.Generic;

namespace HumanArc.Compliance.Shared.Interfaces
{
    public interface ITrainingService
    {
        List<TrainingSummaryViewModel> GetAll();
        TrainingViewModel GetById(int id);
        TrainingViewModel Save(TrainingViewModel model);
        bool Delete(int id);
        TrainingViewModel SetMediaFileName(int id, string filename);
    }
}
