using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Province
    {
        public Province()
        {
            Districts = new HashSet<District>();
        }

        public int Id { get; set; }
        public string? ProvinceName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<District> Districts { get; set; }
    }
}
