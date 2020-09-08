using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class ConditionOperator
    {
        public ConditionOperator()
        {
            Condition = new HashSet<Condition>();
        }

        public int Id { get; set; }
        public string ConditionOperatorName { get; set; }

        public virtual ICollection<Condition> Condition { get; set; }
    }
}
