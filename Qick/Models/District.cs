using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class District
    {
        public District()
        {
            Wards = new HashSet<Ward>();
        }

        public int Id { get; set; }
        public string? DistrictName { get; set; }
        public string? Status { get; set; }
        public int? CityId { get; set; }

        public virtual City? City { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
