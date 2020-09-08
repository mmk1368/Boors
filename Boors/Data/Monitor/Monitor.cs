using Boors;
using Boors.Entities;
using InstaMarket.Web.Core.Repository;

namespace InstaMarket.Web.Core.Data
{
    public partial class DataBase
    {
        private BaseRepository<Monitor> _monitor;
        public BaseRepository<Monitor> Monitor
        {
            get
            {
                if (_monitor == null)
                {
                    _monitor = new BaseRepository<Monitor>(_context);
                }
                return _monitor;
            }
        }
    }
}
