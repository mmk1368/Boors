using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class Operation
    {
        public Operation()
        {
            Condition = new HashSet<Condition>();
        }

        public int Id { get; set; }
        public string OperationName { get; set; }
        public string OperationFaName { get; set; }

        public virtual ICollection<Condition> Condition { get; set; }
    }
}
