using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Fqatopic
    {
        public Fqatopic()
        {
            Fqas = new HashSet<Fqa>();
        }

        public int Id { get; set; }
        public string TopicName { get; set; } = null!;
        public string Status { get; set; } = null!;

        public virtual ICollection<Fqa> Fqas { get; set; }
    }
}
