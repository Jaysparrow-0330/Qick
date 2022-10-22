using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class Result
    {
        public Guid Id { get; set; }
        public int? TestId { get; set; }
        public string? ResultName { get; set; }
        public string? ResultSummary { get; set; }
        public string? ResultRelationship { get; set; }
        public string? ResultSuccessRule { get; set; }
    }
}
