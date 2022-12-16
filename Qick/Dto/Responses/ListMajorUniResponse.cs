namespace Qick.Dto.Responses
{
    public class ListMajorUniResponse
    {
        public Guid Id { get; set; }
        public string? MajorName { get; set; }
        public ICollection<ListUniSpecResponse> spec { get; set; }
    }
}
