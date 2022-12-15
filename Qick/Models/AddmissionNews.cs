using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class AddmissionNews
    {
        public int Id { get; set; }
        public Guid UniId { get; set; }
        public int? UniSpecId { get; set; }
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public string? Status { get; set; }
        public string? Title { get; set; }
        public string? BannerUrl { get; set; }
        public DateTime? CreateDate { get; set; }

        public virtual University Uni { get; set; } = null!;
        public virtual UniversitySpecialization? UniSpec { get; set; }
    }
}
