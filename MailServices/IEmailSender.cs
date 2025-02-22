namespace Authentication.MailServices
{
    public interface IEmailSender
    {
        void SendEmail(string Email);
    }
}
