namespace Qick.Dto.Requests
{
    public class CreateAcademyRequest
    {
        public Guid HighSchoolId { get; set; }
        public int? GraduationYear { get; set; }
        public double? AverageScore { get; set; }
        public string? AcademicRank { get; set; }
    }
}
