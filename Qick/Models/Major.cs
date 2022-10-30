using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Major
    {
        public Major()
        {
            JobMajors = new HashSet<JobMajor>();
            Specializations = new HashSet<Specialization>();
        }

        public Guid Id { get; set; }
        public string? MajorName { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? MajorCode { get; set; }

        public virtual ICollection<JobMajor> JobMajors { get; set; }
        public virtual ICollection<Specialization> Specializations { get; set; }
    }
}
