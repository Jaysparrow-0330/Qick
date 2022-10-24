namespace Qick.Dto.Responses
{
    public class ListTestForAdminResponse
    {
        public string? UserName { get; set; }
        public string? QuizTypeName { get; set; }
        public string? QuizName { get; set; }
        public int? TotalQuestion { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public string? Introduction { get; set; }
        public string? History { get; set; }
        public string? BannerUrl { get; set; }
        public string? CriteriaInformation { get; set; }
        public string? BackgroundUrl { get; set; }
    }
}
