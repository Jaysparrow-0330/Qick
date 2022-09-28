using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Province
    {
        public Province()
        {
            Cities = new HashSet<City>();
        }

        public int Id { get; set; }
        public string? ProvinceName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}
