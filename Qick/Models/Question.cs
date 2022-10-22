using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Question
    {
        public Question()
        {
            Options = new HashSet<Option>();
        }

        public int Id { get; set; }
        public int? TestId { get; set; }
        public string? QuestionContent { get; set; }
        public int? QuestionTypeId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Value { get; set; }

        public virtual QuestionType? QuestionType { get; set; }
        public virtual Test? Test { get; set; }
        public virtual ICollection<Option> Options { get; set; }
    }
}
