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
        public string? Result1 { get; set; }
        public string? Result2 { get; set; }
        public string? Result3 { get; set; }
        public string? Result4 { get; set; }
        public string? Result5 { get; set; }
        public string? Result6 { get; set; }
    }
}
