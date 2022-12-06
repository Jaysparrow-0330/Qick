namespace Qick.Dto.Requests
{
    public class UpdateMajorRequest
    {
        public Guid Id { get; set; }
        public string? MajorName { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? MajorCode { get; set; }
    }
}
