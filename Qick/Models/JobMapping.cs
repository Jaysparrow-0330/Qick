using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class JobMapping
    {
        public int Id { get; set; }
        public int? AttemptId { get; set; }
        public int? JobId { get; set; }
        public bool? Status { get; set; }

        public virtual Attempt? Attempt { get; set; }
        public virtual Job? Job { get; set; }
    }
}
