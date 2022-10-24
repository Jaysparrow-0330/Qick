using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class JobMajor
    {
        public int JobId { get; set; }
        public int MajorId { get; set; }
        public string Status { get; set; } = null!;

        public virtual Job Job { get; set; } = null!;
        public virtual Major Major { get; set; } = null!;
    }
}
