using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Option
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public string? OptionContent { get; set; }
        public string? OptionType { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Value { get; set; }

        public virtual Question? Question { get; set; }
    }
}
