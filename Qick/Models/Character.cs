using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Character
    {
        public Character()
        {
            Attempts = new HashSet<Attempt>();
            JobMappings = new HashSet<JobMapping>();
        }

        public Guid Id { get; set; }
        public int? TestId { get; set; }
        public string? ResultName { get; set; }
        public string? ResultSummary { get; set; }
        public string? ResultRelationship { get; set; }
        public string? ResultSuccessRule { get; set; }
        public string? ResultShortName { get; set; }
        public string? ResultPictureUrl { get; set; }
        public string? ResultCareer { get; set; }
        public string? Value { get; set; }

        public virtual Test? Test { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<JobMapping> JobMappings { get; set; }
    }
}
