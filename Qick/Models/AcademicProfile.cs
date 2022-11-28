using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class AcademicProfile
    {
        /// <summary>
        /// Unique id of academic profile
        /// </summary>
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid HighSchoolId { get; set; }
        public int? GraduationYear { get; set; }
        public double? AverageScore { get; set; }
        public string? AcademicRank { get; set; }
        public string? SchoolReport1Url { get; set; }
        public string? SchoolReport2Url { get; set; }
        public string? SchoolReport3Url { get; set; }
        public string? SchoolReport4Url { get; set; }

        public virtual HighSchool HighSchool { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
