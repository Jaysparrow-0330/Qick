namespace Qick.Dto.Requests
{
    public class UpdateTestRequest
    {
        public int Id { get; set; }
        public string QuizName { get; set; }
        public string? BannerUrl { get; set; }
        public int? TotalQuestion { get; set; }
        public string? Introduction { get; set; }
        public string? History { get; set; }
        public string? CriteriaInformation { get; set; }
        public string? BackgroundUrl { get; set; }
        public string? Status { get; set; }
    }
}
