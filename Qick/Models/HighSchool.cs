using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class HighSchool
    {
        public HighSchool()
        {
            AcademicProfiles = new HashSet<AcademicProfile>();
            ApplicationDetails = new HashSet<ApplicationDetail>();
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string? HighSchoolName { get; set; }
        public string? HighSchoolCode { get; set; }
        public string? Status { get; set; }
        public string? HighSchoolAddress { get; set; }
        public int? WardId { get; set; }

        public virtual Ward? Ward { get; set; }
        public virtual ICollection<AcademicProfile> AcademicProfiles { get; set; }
        public virtual ICollection<ApplicationDetail> ApplicationDetails { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
