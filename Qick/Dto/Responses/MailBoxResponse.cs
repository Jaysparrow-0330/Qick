namespace Qick.Dto.Responses
{
    public class MailBoxResponse
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public string? UserName { get; set; }
        public string? UserAvatarUrl { get; set; }
        public Guid? UniId { get; set; }
        public string? UniName { get; set; }
        public string? UniAvatarUrl { get; set; }
        public string? Topic { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
