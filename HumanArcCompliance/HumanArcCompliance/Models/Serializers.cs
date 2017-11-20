using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HumanArcCompliance.Models
{
    public class Serializers
    {
        public class AddedQuestion
        {
            public int id { get; set; }
            public List<AddedAnswer> answer { get; set; }
        }

        public class AddedAnswer
        {
            public int id { get; set; }
        }
           
    }
}