using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Test
    {
        public Test()
        {
            Attempts = new HashSet<Attempt>();
            Characters = new HashSet<Character>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public Guid CreatorId { get; set; }
        public int? TestTypeId { get; set; }
        public string? TestName { get; set; }
        public int? TotalQuestion { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Introduction { get; set; }
        public string? History { get; set; }
        public string? BannerUrl { get; set; }
        public string? CriteriaInformation { get; set; }
        public string? BackgroundUrl { get; set; }

        public virtual User Creator { get; set; } = null!;
        public virtual TestType? TestType { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<Character> Characters { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
