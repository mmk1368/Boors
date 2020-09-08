using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class TransactionHourlyChangeStage
    {
        public string BourseCode { get; set; }
        public string Action { get; set; }
        public int Count { get; set; }
        public string ShareName { get; set; }
        public long Price { get; set; }
        public string Date { get; set; }
        public DateTime CreationTime { get; set; }
        public long TransactionHourlyChangeStageId { get; set; }
        public byte Status { get; set; }
        public long DailyAccumulativeValue { get; set; }
    }
}
