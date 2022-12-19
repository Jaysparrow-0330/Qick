using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Attempt
    {
        public Attempt()
        {
            AttemptDetails = new HashSet<AttemptDetail>();
        }

        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? TestId { get; set; }
        public DateTime? AttemptDate { get; set; }
        public string? Status { get; set; }
        public string? ResultShortName { get; set; }
        public string? Result1 { get; set; }
        public string? Result2 { get; set; }
        public string? Result3 { get; set; }
        public string? Result4 { get; set; }
        public string? Result5 { get; set; }
        public string? Result6 { get; set; }

        public virtual Test? Test { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<AttemptDetail> AttemptDetails { get; set; }
    }
}
