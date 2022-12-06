namespace Qick.Services.Interfaces
{
    public interface ISendMailService
    {
        Task SendMailAsync(string email, string content, string title, string fileName);
    }
}
