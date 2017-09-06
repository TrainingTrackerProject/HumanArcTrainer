using HumanArc.Compliance.Shared.Entities;
using HumanArc.Compliance.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanArc.Compliance.Shared.Interfaces
{

    public interface IADGroupService : IEntityService<ADGroup>
    {
        ADGroupViewModel DeleteGroup(ADGroupViewModel model);
        ADGroupViewModel AddGroup(ADGroupViewModel model);
    }
}
