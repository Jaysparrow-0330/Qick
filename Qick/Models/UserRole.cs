using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public string Id { get; set; } = null!;
        public string? RoleName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
