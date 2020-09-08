using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class Monitor
    {
        public Monitor()
        {
            MonitorDay = new HashSet<MonitorDay>();
            UserMonitor = new HashSet<UserMonitor>();
        }

        public int Id { get; set; }
        public int ConditionId { get; set; }
        public int UserId { get; set; }
        public int MaxPeriod { get; set; }

        public virtual Condition Condition { get; set; }
        public virtual ICollection<MonitorDay> MonitorDay { get; set; }
        public virtual ICollection<UserMonitor> UserMonitor { get; set; }
    }
}
