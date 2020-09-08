using Boors;
using Boors.Entities;
using InstaMarket.Web.Core.Repository;

namespace InstaMarket.Web.Core.Data
{
    public partial class DataBase
    {
        private BaseRepository<Condition> _condition;
        public BaseRepository<Condition> Condition
        {
            get
            {
                if (_condition == null)
                {
                    _condition = new BaseRepository<Condition>(_context);
                }
                return _condition;
            }
        }
    }
}
