namespace Qick.Dto.Requests
{
    public class UpdateSpecRequest
    {
        public Guid Id { get; set; }
        public Guid? MajorId { get; set; }
        public string? SpecName { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
    }
}
