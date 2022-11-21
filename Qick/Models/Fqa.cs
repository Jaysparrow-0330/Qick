using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Fqa
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UniId { get; set; }
        public string? Fqacontent { get; set; }
        public string? Fqaanswer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public int TopicId { get; set; }

        public virtual FqaTopic Topic { get; set; } = null!;
        public virtual University? Uni { get; set; }
        public virtual User? User { get; set; }
    }
}
