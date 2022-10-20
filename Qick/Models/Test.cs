using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Test
    {
        public Test()
        {
            Attempts = new HashSet<Attempt>();
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public Guid? CreatorId { get; set; }
        public int? QuizTypeId { get; set; }
        public string? QuizName { get; set; }
        public int? TotalQuestion { get; set; }
        public DateTime? CreatedDate { get; set; }
        public bool? Status { get; set; }
        public string? Introrduction { get; set; }
        public string? History { get; set; }
        public string? BannerUrl { get; set; }
        public string? CriteriaInformation { get; set; }

        public virtual User? Creator { get; set; }
        public virtual TestType? QuizType { get; set; }
        public virtual ICollection<Attempt> Attempts { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
