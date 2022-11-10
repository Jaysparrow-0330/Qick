using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Message
    {
        public int Id { get; set; }
        public Guid MailBoxId { get; set; }
        public string MessageContent { get; set; } = null!;
        public string? MessageType { get; set; }

        public virtual MailBox MailBox { get; set; } = null!;
    }
}
