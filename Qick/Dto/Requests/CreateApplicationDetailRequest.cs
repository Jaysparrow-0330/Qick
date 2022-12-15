namespace Qick.Dto.Requests
{
    public class CreateApplicationDetailRequest
    {
        public Guid? ApplicationId { get; set; }
        public string? CredentialFrontImgUrl { get; set; }
        public string? CredentialBackImgUrl { get; set; }
        public Guid HighSchoolId { get; set; }
        public int? GraduationYear { get; set; }
        public double? AverageScore { get; set; }
        public string? AcademicRank { get; set; }
    }
}
