using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class AddmissionNew
    {
        public int Id { get; set; }
        public int? CampaignId { get; set; }
        public int? UniSpecId { get; set; }
        public int? TotalAddmission { get; set; }
        public int? CurrentAddmission { get; set; }
        public bool? Status { get; set; }

        public virtual AddmissionCampaign? Campaign { get; set; }
        public virtual UniversitySpecialization? UniSpec { get; set; }
    }
}
