using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Application
    {
        public Guid? UserId { get; set; }
        public Guid? UniId { get; set; }
        public int? UniSpecId { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string? Status { get; set; }

        public virtual University? Uni { get; set; }
        public virtual UniversitySpecialization? UniSpec { get; set; }
        public virtual User? User { get; set; }
    }
}
