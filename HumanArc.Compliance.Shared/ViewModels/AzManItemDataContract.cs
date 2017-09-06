using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace HumanArc.Foundation.Data.DataContracts
{

   public enum TypeOfAzManItem
    {
        Role=0,
        Operation
    }
    [DataContract]
    [Serializable]
    public class AzManItemDataContract
    {
        [DataMember]
        public int Id { get; set; }
        
        [DataMember]
        public TypeOfAzManItem Type { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

    }
}
