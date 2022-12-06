using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using Qick.Models;
using Qick.Services.Interfaces;

namespace Qick.Services
{
    public class SendMailService : ISendMailService
    {
        private readonly MailSettings _mail;

        public SendMailService(IOptions<MailSettings> mail)
        {
            _mail  = mail.Value;
        }

        public async Task SendMailAsync(string mail, string content, string title, string fileName)
        {
            try
            {
                var email = new MimeMessage();
                email.Sender = MailboxAddress.Parse(_mail.Mail);
                email.To.Add(MailboxAddress.Parse(mail));
                email.Subject = title;
                var builder = new BodyBuilder();
                builder.HtmlBody = GetHtmlBody(fileName, content);
                //builder.TextBody = content;
                email.Body = builder.ToMessageBody();
                using var smtp = new SmtpClient();
                smtp.Authenticate(_mail.Mail, _mail.Password);
                await smtp.SendAsync(email);
                smtp.Disconnect(true);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private string GetHtmlBody(string fileName, string content)
        {
            string body;
            try
            {
                body = File.ReadAllText(fileName);
                body = body.Replace("{CONTENT}", content);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return body;
        }
    }
}
