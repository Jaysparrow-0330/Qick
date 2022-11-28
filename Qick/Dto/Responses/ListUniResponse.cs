namespace Qick.Dto.Responses
{
    public class ListUniResponse
    {
        public Guid Id { get; set; }
        public string? UniName { get; set; }
        public string? UniCode { get; set; }
        public string? AddressNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public string? Vippack { get; set; }
        public string? Status { get; set; }
        public int? WardId { get; set; }
        public string? UniShortName { get; set; }
        public string? Description { get; set; }
    }
}
