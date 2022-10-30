namespace Qick.Dto.Responses
{
    public class ListSpecDbResponse
    {
        public Guid Id { get; set; }
        public Guid? MajorId { get; set; }
        public string? SpecName { get; set; }
        public string? Description { get; set; }

    }
}
