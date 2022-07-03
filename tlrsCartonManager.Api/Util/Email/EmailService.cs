using System.Net.Mail;
using System.Threading.Tasks;

namespace tlrsCartonManager.Api.Util.Email
{
    public interface IEmailService
    {
        void SendEmail(string email, string subject, string body);
    }

    public class EmailService : IEmailService
    {
        private readonly string _host;
        private readonly int _port;
        private readonly bool _enableSSL;
        private readonly string _fromMailAddress;
        private readonly string _username;
        private readonly string _password;

        public EmailService(string host, int port, bool enableSsl, string fromMailAddress, string username, string password)
        {
            _host = host;
            _port = port;
            _enableSSL = enableSsl;
            _fromMailAddress = fromMailAddress;
            _username = username;
            _password = password;
        }

        public void SendEmail(string email, string subject, string body)
        {
            MailMessage mail = new MailMessage();
            SmtpClient client = new SmtpClient(_host);

            mail.From = new MailAddress(_fromMailAddress);
            mail.To.Add(email);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            client.Port = _port;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(_username, _password);
            client.EnableSsl = true;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.TargetName = "STARTTLS/smtp.office365.com";


            client.Send(mail);
        }
    }
}
