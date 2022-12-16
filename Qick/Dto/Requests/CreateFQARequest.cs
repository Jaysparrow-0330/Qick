namespace Qick.Dto.Requests
{
    public class CreateFQARequest
    {
        public string? Fqacontent { get; set; }
        public string? Fqaanswer { get; set; }
        public int TopicId { get; set; }
    }
}
