namespace Qick.Dto.Responses
{
    public class ListApplicationResponse
    {
        public Guid? UserId { get; set; }
        public Guid? UniId { get; set; }
        public int? UniSpecId { get; set; }
        public string? UniName { get; set; }
        public DateTime? ApplyDate { get; set; }
        public string? Status { get; set; }
        public Guid Id { get; set; }
        public string? UniSpecName { get; set; }
        public string? UserName { get; set; }
        public string? Gender { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }
        public string? AvatarUrl { get; set; }
        public string? Email { get; set; }
        public string? AddressNumber { get; set; }
    }
}
