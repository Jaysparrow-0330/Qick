using System;
using System.Collections.Generic;

namespace Qick.Models
{
    public partial class ApplicationDetail
    {
        public Guid? Id { get; set; }
        public Guid? ApplicationId { get; set; }
        public string? CredentialFrontImgUrl { get; set; }
        public string? CredentialBackImgUrl { get; set; }
        public string? HighSchoolCode { get; set; }
        public string? HighSchoolName { get; set; }
        public string? HighSchoolAddress { get; set; }
        public int? GraduationYear { get; set; }
        public double? AvarageScore { get; set; }
        public string? AcademicRank { get; set; }
        public string? SchoolReport1Url { get; set; }
        public string? SchoolReport2Url { get; set; }
        public string? SchoolReport3Url { get; set; }
        public string? SchoolReport4Url { get; set; }

        public virtual Application? Application { get; set; }
    }
}
