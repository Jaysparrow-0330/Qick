using Qick.Models;

namespace Qick.Dto.Responses
{
    public class ApplicationDetailResponse
    {
        public Guid Id { get; set; }
        public Guid? UniId { get; set; }
        public int? UniSpecId { get; set; }
        public Guid? UserId { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? CredentialId { get; set; }
        public string? UniSpecName { get; set; }
        public string? SpecCode { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? AddressNumber { get; set; }
        public ApplicationDetail detail { get; set; }
    }
}
