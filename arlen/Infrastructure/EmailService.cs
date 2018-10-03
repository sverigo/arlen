using MimeKit;
using MailKit.Net.Smtp;

namespace arlen.Infrastructure
{
    public class EmailService
    {
        /* Server success connection variables */
        private string APPLICATION_EMAIL = "noreply.arlen@gmail.com";
        private string APPLICATION_EMAIL_PASS = "ArlenPass";
        private string SMTP_SERVER = "smtp.gmail.com";
        private int SMTP_PORT = 465; // or 587
        private bool SMTP_SSL = true;

        private AccountManager accManager = new AccountManager();

        public bool SendEmail(string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("Arlen Application", APPLICATION_EMAIL));
            emailMessage.To.Add(new MailboxAddress("Arlen Administrator", accManager.GetAccount(1).AccountEmail));
            emailMessage.Subject = subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = message
            };
            try
            {
                using (var client = new SmtpClient())
                {
                    client.Connect(SMTP_SERVER, SMTP_PORT, SMTP_SSL);
                    client.Authenticate(APPLICATION_EMAIL, APPLICATION_EMAIL_PASS);
                    client.Send(emailMessage);

                    client.Disconnect(true);
                }
            }
            catch /* (Exception ex) */
            {
                return false;
            }
            return true;
        }
    }
}