using System.Net.Mail;
using System.Net;
using System.Text;

namespace Authentication.MailServices
{
    public class EmailSender : IEmailSender
    {
        private readonly EmailSettings emailSettings;
        public EmailSender(IConfiguration configuration)
        {
            emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        }
        public void SendEmail(string Email)
        {   
            SmtpClient client = new SmtpClient(emailSettings.smtpServer, emailSettings.port);
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(emailSettings.SendingEmail, emailSettings.AppPassword);

            
            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress("authenticate@gmail.com");
            mailMessage.To.Add(Email);
            mailMessage.Subject = "Here's is 6 digit verification code you requested";
            mailMessage.IsBodyHtml = true;
            StringBuilder mailBody = new StringBuilder();
            mailBody.AppendFormat($"<h1>{455566}</h1>");
            mailBody.AppendFormat("<br />");
            mailBody.AppendFormat("<p>Thank you For Registering account</p>");
            mailMessage.Body = mailBody.ToString();

            // Send email
            client.Send(mailMessage);
        }
    }
}
