namespace Qick.Dto.Requests
{
    public class UpdateFQARequest
    {
        public int Id { get; set; }
        public string? Fqacontent { get; set; }
        public string? Fqaanswer { get; set; }
        public string? Status { get; set; }
        public int TopicId { get; set; }
    }
}
