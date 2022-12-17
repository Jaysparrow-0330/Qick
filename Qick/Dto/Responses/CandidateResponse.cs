namespace Qick.Dto.Responses
{
    public class CandidateResponse
    {
        public Guid HighSchoolId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CredentialId { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? AddressNumber { get; set; }
        public string? CredentialFrontImgUrl { get; set; }
        public string? CredentialBackImgUrl { get; set; }
        public string? HighSchoolCode { get; set; }
        public string? HighSchoolName { get; set; }
        public string? HighSchoolAddress { get; set; }
        public int? GraduationYear { get; set; }
        public double? AvarageScore { get; set; }
        public string? AcademicRank { get; set; }
        public int? WardId { get; set; }
        public int? DistrictId { get; set; }
        public int? ProvinceId { get; set; }
    }
}
