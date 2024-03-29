﻿using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Job
    {
        public Job()
        {
            JobMajors = new HashSet<JobMajor>();
            JobMappings = new HashSet<JobMapping>();
        }

        public int Id { get; set; }
        public string? JobName { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }

        public virtual ICollection<JobMajor> JobMajors { get; set; }
        public virtual ICollection<JobMapping> JobMappings { get; set; }
    }
}
