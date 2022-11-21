namespace Qick.Dto.Requests
{
    public class CreateMessRequest
    {
        public Guid recipientId { get; set; }
        public string MessageContent { get; set; }

        public string? Topic { get; set; }
    }
}
