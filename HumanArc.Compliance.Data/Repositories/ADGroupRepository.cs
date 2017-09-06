using HumanArc.Compliance.Data.Database;
using HumanArc.Compliance.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanArc.Compliance.Data.Repositories
{
    public class ADGroupRepository : RepositoryBase<ADGroup>
    {
        private readonly ComplianceContext _context;
        public ADGroupRepository(ComplianceContext context, IDatabaseFactory databaseFactory) : base(databaseFactory)
        {
            _context = context;
        }

        public ADGroup GetGroupByGroupName(string groupName)
        {
            //List<ADGroup> adgroupList = DatabaseFactory.Get().Groups.ToList().FindAll(x => x.GroupName == groupName);  
            return DatabaseFactory.Get().Groups.ToList().FirstOrDefault(x => x.GroupName == groupName);

        }
    }
}
