using Microsoft.EntityFrameworkCore;
using Qick.Dto.Requests;
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

        public async Task<MailBox> CreateMail(CreateMessRequest request,Guid userId, string type)
        {
            try
            {
                MailBox addResult = new()
                {
                   Id = Guid.NewGuid(),
                   UniId = request.recipientId,
                   UserId = userId,
                   Topic = request.Topic,
                   CreateDate = DateTime.Now,
                   Type = type
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

        public async Task<IEnumerable<MailBox>> GetMailBoxByUniId(Guid? uniId)
        {
            try
            {
                var result = await _context.MailBoxes
                                    .Where(a => a.UniId == uniId)
                                    .Include(a => a.Uni)
                                    .Include(x => x.User)
                                    .OrderBy(x => x.CreateDate)
                                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<MailBox>> GetMailBoxByUserId(Guid? userId)
        {
            try
            {
                var result = await _context.MailBoxes
                                    .Where(a => a.UserId == userId)
                                    .Include(a => a.Uni)
                                    .Include(x => x.User)
                                    .OrderBy(x => x.CreateDate)
                                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<Message>> GetMessByMailId(Guid? MailId)
        {
            try
            {
                var result = await _context.Messages
                                    .Where(a => a.MailBoxId == MailId)
                                    .ToListAsync();
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
