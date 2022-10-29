namespace Qick.Dto.Requests
{
    public class JobCharMappingRequest
    {
        public Guid CharacterId { get; set; }
        public ICollection<int> JobIds { get; set; }
    }
}
