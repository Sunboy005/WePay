namespace wepay.EmailService
{
    public interface IEmailSender
    {
        Task SendEmailAsync(Message message);
    }
}
