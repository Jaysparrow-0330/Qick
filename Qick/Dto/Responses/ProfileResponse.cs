namespace Qick.Dto.Responses
{
    public class ProfileResponse
    {
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? CredentialId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? Status { get; set; }
        public string? AddressNumber { get; set; }
        public string? CredentialFrontImgUrl { get; set; }
        public string? CredentialBackImgUrl { get; set; }
        public string HighSchoolName { get; set; }
        public string? WardName { get; set; }
        public string? ProvinceName { get; set; }
        public string? DistrictName { get; set; }
        public string? PublicProfile { get; set; }
    }
}
