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
        public int? ProvinceId { get; set; }
        public string? DistrictName { get; set; }
        public int DistrictType { get; set; }

        public virtual Province? Province { get; set; }
        public virtual ICollection<Ward> Wards { get; set; }
    }
}
