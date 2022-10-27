﻿using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Ward
    {
        public Ward()
        {
            Universities = new HashSet<University>();
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string WardName { get; set; } = null!;
        public bool? Status { get; set; }
        public int? DistrictId { get; set; }

        public virtual District? District { get; set; }
        public virtual ICollection<University> Universities { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
