namespace Qick.Dto.Requests
{
    public class JobMajorMappingRequest
    {
        public int JobId { get; set; }
        public ICollection<Guid> MajorIds { get; set; }
    }
}
