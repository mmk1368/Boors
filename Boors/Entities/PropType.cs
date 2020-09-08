using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class PropType
    {
        public PropType()
        {
            Condition = new HashSet<Condition>();
        }

        public int Id { get; set; }
        public string Type { get; set; }

        public virtual ICollection<Condition> Condition { get; set; }
    }
}
