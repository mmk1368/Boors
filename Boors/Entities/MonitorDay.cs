using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class MonitorDay
    {
        public int Id { get; set; }
        public string DayName { get; set; }
        public byte DayId { get; set; }
        public int MonitorId { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public virtual Monitor Monitor { get; set; }
    }
}
