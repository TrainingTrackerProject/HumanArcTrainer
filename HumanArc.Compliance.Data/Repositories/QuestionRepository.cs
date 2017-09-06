using HumanArc.Compliance.Data.Database;
using HumanArc.Compliance.Shared.Entities;

namespace HumanArc.Compliance.Data.Repositories
{
    public class QuestionRepository : RepositoryBase<Question>
    {
        private readonly ComplianceContext _context;
        public QuestionRepository(ComplianceContext context, IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _context = context;
        }
    }
}
