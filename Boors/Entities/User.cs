using System;
using System.Collections.Generic;

namespace Boors.Entities
{
    public partial class User
    {
        public User()
        {
            UserMonitor = new HashSet<UserMonitor>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        public virtual ICollection<UserMonitor> UserMonitor { get; set; }
    }
}
