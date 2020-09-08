using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class Parameter
    {
        public Parameter()
        {
            Condition = new HashSet<Condition>();
        }

        public int Id { get; set; }
        public string ParameterName { get; set; }
        public string ParameterFaName { get; set; }

        public virtual ICollection<Condition> Condition { get; set; }
    }
}
