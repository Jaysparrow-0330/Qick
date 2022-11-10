using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class MailBox
    {
        public MailBox()
        {
            Messages = new HashSet<Message>();
        }

        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UniId { get; set; }

        public virtual University? Uni { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
