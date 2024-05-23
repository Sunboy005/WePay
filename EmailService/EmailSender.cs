using MimeKit;
using System.Linq.Expressions;
using System.Net;
using System.Net.Mail;

namespace wepay.EmailService
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailConfiguration _configuration;

        public EmailSender(EmailConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task SendEmailAsync(Message message)
        {
            var emailMessage = CreateEmailMessage(message);

            await _SendEmailAsync(emailMessage);
           
        }

        private MailMessage CreateEmailMessage(Message message)
        {
            var emailMessage = new MailMessage();
            emailMessage.From = new MailAddress(_configuration.From);
            emailMessage.To.Add(message.To.First());
            emailMessage.Subject = message.Subject;
            emailMessage.Body = message.Body;
            emailMessage.Priority = MailPriority.Normal;
            return emailMessage;

        }

        private async Task _SendEmailAsync(MailMessage message)
        {
            using (var client = new SmtpClient(_configuration.From, _configuration.Port))
            {                
                try
                {
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential(_configuration.From, _configuration.Password);
                     await client.SendMailAsync(message);
                    
                }
                catch(Exception ex)
                {

                    throw new Exception("Couldn't Send Email");
                }
                finally
                {
                    client.Dispose();
                }
            }
        }
    }
}
