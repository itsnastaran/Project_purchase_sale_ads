using Microsoft.AspNetCore.Identity.UI.Services;
using Projectcore.Data;
using System.Net;
using System.Net.Mail;

namespace Projectcore.Service
{
    public class EmailSender:IEmailSender
    {
        private readonly ApplicationDbContext _db;

        public EmailSender(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task SendEmailAsync(string email, string subject, string message)
        {
            var q = _db.ConfigEmails.FirstOrDefault();
            using(MailMessage mail=new MailMessage(q.Username,email))
            {
                mail.Subject = subject;
                mail.Body = message;
                mail.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient(q.Smtp);
                smtp.EnableSsl = true;
                NetworkCredential network = new NetworkCredential(q.Username, q.Password);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = network;
                smtp.Port = q.Port;
                smtp.Send(mail);
            }
            return Task.FromResult(0);
        }
    }
}
