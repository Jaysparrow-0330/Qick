using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class AttemptDetail
    {
        public int AttemptId { get; set; }
        public int OptionId { get; set; }
        public string? Status { get; set; }
        public int Id { get; set; }
        public string? SelectedField { get; set; }

        public virtual Attempt Attempt { get; set; } = null!;
        public virtual Option Option { get; set; } = null!;
    }
}
