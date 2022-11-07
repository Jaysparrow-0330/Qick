using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class UserRole
    {
        public string Id { get; set; } = null!;
        public string? RoleName { get; set; }
        public string? Status { get; set; }
    }
}
