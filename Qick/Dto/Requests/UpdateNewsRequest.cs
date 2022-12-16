namespace Qick.Dto.Requests
{
    public class UpdateNewsRequest
    {
        public int? Id { get; set; }
        public int? UniSpecId { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? BannerUrl { get; set; }
        public string? Status { get; set; }
    }
}
