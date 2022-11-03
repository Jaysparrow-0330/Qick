using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class HighSchool
    {
        public HighSchool()
        {
            Users = new HashSet<User>();
        }

        public Guid Id { get; set; }
        public string? HighSchoolName { get; set; }
        public string? HighSchoolCode { get; set; }
        public string? Status { get; set; }
        public string? HighSchoolAddress { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
