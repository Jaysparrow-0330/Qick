namespace Qick.Dto.Responses
{
    public class TakingTestResponse
    {
        public int Id { get; set; }
        public string TestName { get; set; }
        public string? BannerUrl { get; set; }
        public ICollection<QuestionResponse> questions { get; set; }
    }
}
