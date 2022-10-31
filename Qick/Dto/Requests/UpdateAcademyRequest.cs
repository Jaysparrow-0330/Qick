namespace Qick.Dto.Requests
{
    public class UpdateAcademyRequest
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
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
    }
}
