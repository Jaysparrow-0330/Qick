namespace Qick.Dto.Requests
{
    public class CreateMessRequest
    {
        public Guid uniId { get; set; }
        public string MessageContent { get; set; }

        public string? Topic { get; set; }
    }
}
