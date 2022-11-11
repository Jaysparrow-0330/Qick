namespace Qick.Dto.Requests
{
    public class CreateReplyRequest
    {
        public Guid MailBoxId { get; set; }
        public string MessageContent { get; set; }
    }
}
