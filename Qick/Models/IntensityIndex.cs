using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class IntensityIndex
    {
        public int Id { get; set; }
        public double Percentage { get; set; }
        public int Value { get; set; }
        public int Segment { get; set; }
        public string Dimension { get; set; } = null!;
    }
}
