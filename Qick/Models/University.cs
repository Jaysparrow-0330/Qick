using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class University
    {
        public University()
        {
            AddmissionCampaigns = new HashSet<AddmissionCampaign>();
            Fqas = new HashSet<Fqa>();
            UniversitySpecializations = new HashSet<UniversitySpecialization>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string? UniName { get; set; }
        public string? AddressNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Vippack { get; set; }
        public string? Status { get; set; }
        public string? UniCode { get; set; }
        public string? Description { get; set; }
        public int? CountSaved { get; set; }

        public virtual ICollection<AddmissionCampaign> AddmissionCampaigns { get; set; }
        public virtual ICollection<Fqa> Fqas { get; set; }
        public virtual ICollection<UniversitySpecialization> UniversitySpecializations { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
