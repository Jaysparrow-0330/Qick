using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Attempt
    {
        public Attempt()
        {
            JobMappings = new HashSet<JobMapping>();
        }

        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? QuizId { get; set; }
        public DateTime? AttemptDate { get; set; }
        public string? Result { get; set; }
        public string? Status { get; set; }

        public virtual Test? Quiz { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<JobMapping> JobMappings { get; set; }
    }
}
