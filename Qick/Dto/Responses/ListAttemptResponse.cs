namespace Qick.Dto.Responses
{
    public class ListAttemptResponse
    {
        public int id { get; set; }
        public int testId { get; set; }
        public string testName { get; set; }
        public DateTime? AttemptDate { get; set; }
        public string? ResultShortName { get; set; }
        public string? ResultName { get; set; }
    }
}
