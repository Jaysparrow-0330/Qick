using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class City
    {
        public City()
        {
            Districts = new HashSet<District>();
        }

        public int Id { get; set; }
        public string? CityName { get; set; }
        public bool? Status { get; set; }
        public int? ProvinceId { get; set; }

        public virtual Province? Province { get; set; }
        public virtual ICollection<District> Districts { get; set; }
    }
}
