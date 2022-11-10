namespace Qick.Dto.Responses
{
    public class UniDetailResponse
    {
        public Guid Id { get; set; }
        public string? UniName { get; set; }
        public string? AddressNumber { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? WebsiteUrl { get; set; }
        public string? AvatarUrl { get; set; }
        public string? CoverPhotoUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Vippack { get; set; }
        public string? Status { get; set; }
        public string? UniCode { get; set; }
        public string? Description { get; set; }
        public int? CountSaved { get; set; }
        public int? WardId { get; set; }
        public int? ProvinceId { get; set; }
        public int? DistrictId { get; set; }
    }
}
