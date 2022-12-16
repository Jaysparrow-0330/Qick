namespace Qick.Dto.Responses
{
    public class FQAResponse
    {
        public int Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? UniId { get; set; }
        public string? Fqacontent { get; set; }
        public string? Fqaanswer { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string? Status { get; set; }
        public int TopicId { get; set; }
    }
}
