using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class AddmissionNew
    {
        public int Id { get; set; }
        public Guid UniId { get; set; }
        public int? UniSpecId { get; set; }
        public int? TotalAddmission { get; set; }
        public int? CurrentAddmission { get; set; }
        public string? Status { get; set; }

        public virtual AddmissionCampaign Uni { get; set; } = null!;
        public virtual University UniNavigation { get; set; } = null!;
        public virtual UniversitySpecialization? UniSpec { get; set; }
    }
}
