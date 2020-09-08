using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class Operator
    {
        public Operator()
        {
            Condition = new HashSet<Condition>();
        }

        public int Id { get; set; }
        public string OperatorName { get; set; }
        public string OperatorFaName { get; set; }

        public virtual ICollection<Condition> Condition { get; set; }
    }
}
