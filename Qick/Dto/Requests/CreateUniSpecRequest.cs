namespace Qick.Dto.Requests
{
    public class CreateUniSpecRequest
    {
        public Guid? UniId { get; set; }
        public Guid? SpecId { get; set; }
        public string? UniSpecName { get; set; }
        public string? SpecCode { get; set; }
    }
}
