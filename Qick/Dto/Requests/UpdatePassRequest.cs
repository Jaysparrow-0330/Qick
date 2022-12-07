namespace Qick.Dto.Requests
{
    public class UpdatePassRequest
    {
        public string email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
