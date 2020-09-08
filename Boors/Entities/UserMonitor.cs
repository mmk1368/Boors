using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class UserMonitor
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MonitorId { get; set; }

        public virtual Monitor Monitor { get; set; }
        public virtual User User { get; set; }
    }
}
