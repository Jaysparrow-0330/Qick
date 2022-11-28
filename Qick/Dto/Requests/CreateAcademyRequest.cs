namespace Qick.Dto.Requests
{
    public class CreateAcademyRequest
    {
        public Guid HighSchoolId { get; set; }
        public int? GraduationYear { get; set; }
        public double? AverageScore { get; set; }
        public string? AcademicRank { get; set; }
        public string? SchoolReport1Url { get; set; }
        public string? SchoolReport2Url { get; set; }
        public string? SchoolReport3Url { get; set; }
        public string? SchoolReport4Url { get; set; }
    }
}
