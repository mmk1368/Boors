using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class Condition
    {
        public Condition()
        {
            InverseFirstChild = new HashSet<Condition>();
            InverseSecondChild = new HashSet<Condition>();
            Monitor = new HashSet<Monitor>();
        }

        public int Id { get; set; }
        public string FieldName { get; set; }
        public int? OperatorId { get; set; }
        public int? OperationId { get; set; }
        public string PropName { get; set; }
        public int? PropTypeId { get; set; }
        public string Variable1 { get; set; }
        public string Variable2 { get; set; }
        public int? Period { get; set; }
        public int? FirstChildId { get; set; }
        public int? SecondChildId { get; set; }
        public int? ConditionOperatorId { get; set; }

        public virtual ConditionOperator ConditionOperator { get; set; }
        public virtual Condition FirstChild { get; set; }
        public virtual Operation Operation { get; set; }
        public virtual Operator Operator { get; set; }
        public virtual Condition SecondChild { get; set; }
        public virtual ICollection<Condition> InverseFirstChild { get; set; }
        public virtual ICollection<Condition> InverseSecondChild { get; set; }
        public virtual ICollection<Monitor> Monitor { get; set; }
    }
}
