using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;

namespace WebApplication1.Source.Svc
{
    public class EmailService
    {
        private readonly string gmailUsername;
        private readonly string gmailAppPassword;

        public EmailService(string username, string appPassword)
        {
            gmailUsername = username;
            gmailAppPassword = appPassword;
        }

        public async Task SendEmailAsync(string toEmail, string subject, string body)
        {
            using (SmtpClient smtpClient = new SmtpClient("smtp.gmail.com"))
            {
                smtpClient.Port = 587;
                smtpClient.Credentials = new NetworkCredential(gmailUsername, gmailAppPassword);
                smtpClient.EnableSsl = true;

                using (MailMessage mailMessage = new MailMessage())
                {
                    mailMessage.From = new MailAddress(gmailUsername);
                    mailMessage.To.Add(toEmail);
                    mailMessage.Subject = subject;
                    mailMessage.Body = body;
                    mailMessage.IsBodyHtml = true;

                    await smtpClient.SendMailAsync(mailMessage);
                }
            }
        }
    }
}
