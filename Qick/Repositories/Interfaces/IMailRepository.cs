﻿using Qick.Dto.Requests;
using Qick.Models;

namespace Qick.Repositories.Interfaces
{
    public interface IMailRepository
    {
        // create 
        Task<MailBox> CreateMail(CreateMessRequest request, Guid userId, string type);
        // create 
        Task<MailBox> CreateMailUni(CreateMessRequest request, Guid uniId, string type);
        // create 
        Task<bool> CreateMess(Guid mailId, string content, string type);

        // get 
        Task<IEnumerable<MailBox>> GetMailBoxByUniId(Guid? uniId);
        // get 
        Task<IEnumerable<MailBox>> GetMailBoxByUserId(Guid? userId);

        // get 
        Task<IEnumerable<Message>> GetMessByMailId(Guid? MailId);
    }
}
