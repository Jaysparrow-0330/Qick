using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Attempt
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public int? QuizId { get; set; }
        public DateTime? AttemptDate { get; set; }
        public Guid? CharacterId { get; set; }
        public string? Status { get; set; }

        public virtual Character? Character { get; set; }
        public virtual Test? Quiz { get; set; }
        public virtual User? User { get; set; }
    }
}
