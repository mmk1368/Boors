using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Boors.Services
{
    public class TimeRound
    {
        public static DateTime TimeRoundDown(DateTime now, TimeSpan timeSpan)
        {
            return new DateTime((now.Ticks) / timeSpan.Ticks * timeSpan.Ticks, now.Kind);
        }
    }
}
