using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class JobMajor
    {
        public int JobId { get; set; }
        public Guid MajorId { get; set; }
        public string Status { get; set; } = null!;

        public virtual Major Major { get; set; } = null!;
    }
}
