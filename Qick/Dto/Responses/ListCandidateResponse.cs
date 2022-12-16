namespace Qick.Dto.Responses
{
    public class ListCandidateResponse
    {
        public Guid Id { get; set; }
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
        public int? WardId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
    }
}
