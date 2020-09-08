using Boors.Models;
using System;

namespace InstaMarket.Web.Core.Data
{
    public partial class DataBase : IDisposable
    {
        BourseContext _context = new BourseContext();
        public void Dispose()
        {
            _context.DisposeAsync();
        }
    }
}