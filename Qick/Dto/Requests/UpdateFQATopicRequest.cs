namespace Qick.Dto.Requests
{
    public class UpdateFQATopicRequest
    {
        public int Id { get; set; }
        public string TopicName { get; set; } = null!;
        public string Status { get; set; } = null!;
    }
}
