using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IMailRepository
    {
        // create 
        Task<MailBox> CreateMail(Guid uniId, Guid userId);

        // create 
        Task<bool> CreateMess(Guid mailId, string content, string type);
    }
}
