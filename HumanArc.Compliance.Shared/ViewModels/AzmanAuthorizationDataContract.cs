using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HumanArc.Foundation.Data.DataContracts
{
    [DataContract]
    [Serializable]
    public class AzmanAuthorizationDataContract
    {
        [DataMember] public string DisplayName { get; set; }
        [DataMember] public string UserName { get; set; }
        [DataMember] public string SID { get; set; }
    }
}
