using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class AttemptDetail
    {
        public int? AttemptId { get; set; }
        public int? OptionId { get; set; }
        public bool? Status { get; set; }

        public virtual Attempt? Attempt { get; set; }
        public virtual Option? Option { get; set; }
    }
}
