using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Attempt
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? TestId { get; set; }
        public DateTime? AttemptDate { get; set; }
        public string? Status { get; set; }
        public string? ResultShortName { get; set; }

        public virtual Test? Test { get; set; }
        public virtual User? User { get; set; }
    }
}
