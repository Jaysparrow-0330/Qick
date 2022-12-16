namespace Qick.Dto.Responses
{
    public class ListNewsResponse
    {
        public int Id { get; set; }
        public Guid UniId { get; set; }
        public int? UniSpecId { get; set; }
        public string? Content { get; set; }
        public Guid UserId { get; set; }
        public string? Status { get; set; }
        public string? Title { get; set; }
        public string? BannerUrl { get; set; }
        public DateTime? CreateDate { get; set; }

        public string? UniSpecName { get; set; }
    }
}
