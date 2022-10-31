namespace Qick.Dto.Requests
{
    public class UserProfileUpdateRequest
    {
        public Guid Id { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? CredentialId { get; set; }
        public string? AvatarUrl { get; set; }
        public string? AddressNumber { get; set; }
        public string? CredentialFrontImgUrl { get; set; }
        public string? CredentialBackImgUrl { get; set; }
    }
}
