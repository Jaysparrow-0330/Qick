using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class UserOtp
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string? Otp { get; set; }
        public DateTime? CreateDate { get; set; }
        public DateTime? ValidateUntil { get; set; }
        public string? Status { get; set; }

        public virtual User? User { get; set; }
    }
}
