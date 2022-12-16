namespace Qick.Dto.Responses
{
    public class ListUniSpecResponse
    {
        public int Id { get; set; }
        public Guid? UniId { get; set; }
        public Guid? SpecId { get; set; }
        public string? UniSpecName { get; set; }
    }
}
