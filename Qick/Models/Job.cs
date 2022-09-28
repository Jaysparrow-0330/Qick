using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Job
    {
        public Job()
        {
            JobMappings = new HashSet<JobMapping>();
        }

        public int Id { get; set; }
        public string? JobName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<JobMapping> JobMappings { get; set; }
    }
}
