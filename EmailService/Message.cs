using MimeKit;
using System.Net.Mail;

namespace wepay.EmailService
{
    public class Message
    {
        public List<MailAddress> To { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public Message(IEnumerable<string> to, string subject, string body) { 
            To = new List<MailAddress>();

            To.AddRange(to.Select(x => new MailAddress(string.Empty, x))); 
            Subject = subject;
            Body = body;
        
        }
    }
}
