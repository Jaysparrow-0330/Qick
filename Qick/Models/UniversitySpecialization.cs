using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class UniversitySpecialization
    {
        public UniversitySpecialization()
        {
            AddmissionNews = new HashSet<AddmissionNew>();
            Applications = new HashSet<Application>();
        }

        public int Id { get; set; }
        public Guid? UniId { get; set; }
        public Guid? SpecId { get; set; }
        public string? UniSpecName { get; set; }
        public string? Status { get; set; }
        public string? SpecCode { get; set; }

        public virtual Specialization? Spec { get; set; }
        public virtual University? Uni { get; set; }
        public virtual ICollection<AddmissionNew> AddmissionNews { get; set; }
        public virtual ICollection<Application> Applications { get; set; }
    }
}
