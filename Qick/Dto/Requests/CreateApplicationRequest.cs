namespace Qick.Dto.Requests
{
    public class CreateApplicationRequest
    {
        public Guid? UserId { get; set; }
        public Guid? UniId { get; set; }
        public int? UniSpecId { get; set; }
    }
}
