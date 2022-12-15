namespace Qick.Dto.Requests
{
    public class CreateNewsRequest
    {
        public int? UniSpecId { get; set; }
        public string? Content { get; set; }
        public string? Title { get; set; }
        public string? BannerUrl { get; set; }
    }
}
