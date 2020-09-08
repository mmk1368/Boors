using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class Transaction
    {
        public string BourseCode { get; set; }
        public string FullName { get; set; }
        public string Action { get; set; }
        public int Count { get; set; }
        public string ShareName { get; set; }
        public string Precedence { get; set; }
        public long Price { get; set; }
        public string RootGroup { get; set; }
        public string SellerBuyer { get; set; }
        public string RequestCode { get; set; }
        public string BranchName { get; set; }
        public string Date { get; set; }
        public string Declaration { get; set; }
        public long TransactionId { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
