using Qick.Models;
using Qick.Repositories.Interfaces;

namespace Qick.Repositories
{
    public class MailRepository : IMailRepository
    {
        private readonly QickDatabaseManangementContext _context;

        public MailRepository(QickDatabaseManangementContext context)
        {
            _context = context;
        }

        public async Task<MailBox> CreateMail(Guid uniId,Guid userId)
        {
            try
            {
                MailBox addResult = new()
                {
                   Id = Guid.NewGuid(),
                   UniId = uniId,
                   UserId = userId
                };
                await _context.MailBoxes.AddAsync(addResult);
                await _context.SaveChangesAsync();
                return addResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CreateMess(Guid mailId, string content, string type)
        {
            try
            {
                Message addResult = new()
                {
                    MailBoxId = mailId,
                    MessageContent = content,
                    MessageType = type,
                    CreateDate = DateTime.Now
                };
                await _context.Messages.AddAsync(addResult);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
