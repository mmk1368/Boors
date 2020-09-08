using Boors;
using Boors.Entities;
using InstaMarket.Web.Core.Repository;

namespace InstaMarket.Web.Core.Data
{
    public partial class DataBase
    {
        private BaseRepository<TransactionHourlyChange> _transactionHourlyChange;
        public BaseRepository<TransactionHourlyChange> TransactionHourlyChange
        {
            get
            {
                if (_transactionHourlyChange == null)
                {
                    _transactionHourlyChange = new BaseRepository<TransactionHourlyChange>(_context);
                }
                return _transactionHourlyChange;
            }
        }
    }
}
