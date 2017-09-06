using HumanArc.Compliance.Data.Database;
using HumanArc.Compliance.Shared.Entities;
using System;
using System.Linq;

namespace HumanArc.Compliance.Data.Repositories
{
    public class TrainingRepository : RepositoryBase<Training>
    {
        private readonly ComplianceContext _context;
        public TrainingRepository(ComplianceContext context, IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _context = context;
        }

        //public bool AssociateAndSave(int trainingId, ADGroup group)
        //{
        //    var training = _context.Trainings.FirstOrDefault(x => x.ID == trainingId);
        //    var existingGroup = _context.Groups.FirstOrDefault(x => x.ID == group.ID);

        //    if (existingGroup != null)
        //    {
        //        training.ADGroup.Add(existingGroup);
        //        _context.Trainings.Attach(training);
        //        _context.SaveChanges();

        //        return true;
        //    }

        //    return false;
        //}
    }
}
