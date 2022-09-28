using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class JobMajor
    {
        public int JobId { get; set; }
        public int MajorId { get; set; }
        public bool Status { get; set; }

        public virtual Job Job { get; set; } = null!;
        public virtual Major Major { get; set; } = null!;
    }
}
