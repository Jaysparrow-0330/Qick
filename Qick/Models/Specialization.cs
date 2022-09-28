using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Specialization
    {
        public Specialization()
        {
            UniversitySpecializations = new HashSet<UniversitySpecialization>();
        }

        public string Id { get; set; } = null!;
        public int? MajorId { get; set; }
        public string? SpecName { get; set; }
        public bool? Status { get; set; }

        public virtual Major? Major { get; set; }
        public virtual ICollection<UniversitySpecialization> UniversitySpecializations { get; set; }
    }
}
