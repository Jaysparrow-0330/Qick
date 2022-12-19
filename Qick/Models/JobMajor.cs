using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class JobMajor
    {
        public int? JobId { get; set; }
        public Guid? MajorId { get; set; }
        public string? Status { get; set; }
        public int Id { get; set; }

        public virtual Job? Job { get; set; }
        public virtual Major? Major { get; set; }
    }
}
