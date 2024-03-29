﻿using System;
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
        public string? Topic { get; set; }
        public DateTime? CreateDate { get; set; }
        public string? Type { get; set; }

        public virtual University? Uni { get; set; }
        public virtual User? User { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
