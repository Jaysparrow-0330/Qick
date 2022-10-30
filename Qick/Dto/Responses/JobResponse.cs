namespace Qick.Dto.Responses
{
    public class JobResponse
    {
        public int Id { get; set; }
        public string? JobName { get; set; }
        public string? Status { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
    }
}
