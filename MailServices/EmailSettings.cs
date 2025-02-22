namespace Authentication.MailServices
{
    public class EmailSettings
    {
        public string? smtpServer {  get; set; }
        public int port { get; set; }
        public string ?SendingEmail { get; set; }
        public string ?AppPassword { get; set; }
    }
}
