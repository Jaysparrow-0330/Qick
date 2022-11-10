using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class AddmissionCampaign
    {
        public AddmissionCampaign()
        {
            AddmissionNews = new HashSet<AddmissionNew>();
        }

        public Guid Id { get; set; }
        public Guid? UniId { get; set; }
        public int? TotalAddmission { get; set; }
        public int? CurrentAddmission { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }

        public virtual University? Uni { get; set; }
        public virtual ICollection<AddmissionNew> AddmissionNews { get; set; }
    }
}
