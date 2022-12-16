namespace Qick.Dto.Responses
{
    public class ListFqaResponse
    {
        public int Id { get; set; }
        public string TopicName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public ICollection<FQAResponse> Fqas { get; set; }
    }
}
