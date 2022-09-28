using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Major
    {
        public Major()
        {
            Specializations = new HashSet<Specialization>();
        }

        public int Id { get; set; }
        public int? JobId { get; set; }
        public string? MajorName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Specialization> Specializations { get; set; }
    }
}
