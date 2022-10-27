using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class JobMapping
    {
        public int Id { get; set; }
        public Guid? CharacterId { get; set; }
        public int? JobId { get; set; }
        public string? Status { get; set; }

        public virtual Character? Character { get; set; }
        public virtual Job? Job { get; set; }
    }
}
