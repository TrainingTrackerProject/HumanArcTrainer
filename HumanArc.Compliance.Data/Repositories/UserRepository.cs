using HumanArc.Compliance.Data.Database;
using HumanArc.Compliance.Shared.Entities;
using System.Data.Entity;
using System.Linq;

namespace HumanArc.Compliance.Data.Repositories
{
    public class UserRepository : RepositoryBase<User>
    {
        private readonly ComplianceContext _context;
        public UserRepository(ComplianceContext context, IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _context = context;
        }

        public User GetUserByUserName(string userName)
        {
            return  DatabaseFactory.Get().Users.ToList().FirstOrDefault(x => x.UserName == userName);

        }
    }
}
