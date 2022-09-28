using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class UniversitySpecialization
    {
        public UniversitySpecialization()
        {
            AddmissionNews = new HashSet<AddmissionNew>();
        }

        public int Id { get; set; }
        public Guid? UniId { get; set; }
        public string? SpecId { get; set; }
        public string? UniSpecName { get; set; }
        public bool? Status { get; set; }

        public virtual Specialization? Spec { get; set; }
        public virtual University? Uni { get; set; }
        public virtual ICollection<AddmissionNew> AddmissionNews { get; set; }
    }
}
