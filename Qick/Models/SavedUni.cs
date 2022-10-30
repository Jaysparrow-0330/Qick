using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class SavedUni
    {
        public Guid? UniversityId { get; set; }
        public Guid? UserId { get; set; }
        public string? Status { get; set; }
        public int Id { get; set; }

        public virtual University? University { get; set; }
        public virtual User? User { get; set; }
    }
}
