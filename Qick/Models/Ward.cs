using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Ward
    {
        public int Id { get; set; }
        public string WardName { get; set; } = null!;
        public bool? Status { get; set; }
        public int? DistrictId { get; set; }

        public virtual District? District { get; set; }
    }
}
