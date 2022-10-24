using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class RootTestKind
    {
        public Guid Id { get; set; }
        public string? RootTestName { get; set; }
        public int? NumberOfVersion { get; set; }
    }
}
