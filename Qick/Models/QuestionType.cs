using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        public string? TypeName { get; set; }
        public bool? Status { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
