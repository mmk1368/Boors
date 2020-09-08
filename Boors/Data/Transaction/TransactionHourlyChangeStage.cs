using Boors;
using Boors.Entities;
using InstaMarket.Web.Core.Repository;

namespace InstaMarket.Web.Core.Data
{
    public partial class DataBase
    {
        private BaseRepository<TransactionHourlyChangeStage> _TransactionHourlyChangeStage;
        public BaseRepository<TransactionHourlyChangeStage> TransactionHourlyChangeStage
        {
            get
            {
                if (_TransactionHourlyChangeStage == null)
                {
                    _TransactionHourlyChangeStage = new BaseRepository<TransactionHourlyChangeStage>(_context);
                }
                return _TransactionHourlyChangeStage;
            }
        }
    }
}
