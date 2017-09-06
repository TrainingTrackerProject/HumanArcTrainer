using HumanArc.Compliance.Data.Database;
using HumanArc.Compliance.Shared.Entities;

namespace HumanArc.Compliance.Data.Repositories
{
    public class ScheduleRepository : RepositoryBase<Schedule>
    {
        private readonly ComplianceContext _context;
        public ScheduleRepository(ComplianceContext context, IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _context = context;
        }
    }
}
