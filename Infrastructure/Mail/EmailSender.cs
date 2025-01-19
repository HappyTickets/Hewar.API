using Infrastructure.Persistence.Configurations;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Mail
{
    internal class EmailSender : IEmailSender
    {
        private readonly ILogger<EmailSender> _logger;
        private readonly MailSettings _emailSettings;

        public EmailSender(ILogger<EmailSender> logger, IOptions<MailSettings> emailSettings)
        {
            _logger = logger;
            _emailSettings = emailSettings.Value;
        }

        public async Task SendAsync(string to, string subject, SuccessCodes successCodes, string token)
        {
            //create email message
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.DisplayName, _emailSettings.UserName));
            message.To.Add(MailboxAddress.Parse(to));
            message.Subject = subject;
            message.Body = new TextPart(TextFormat.Html)
            {
                Text = $"{(int)successCodes}: {token}"
            };

            //send email message
            using var smtpClient = new SmtpClient();
            await smtpClient.ConnectAsync(_emailSettings.Host, _emailSettings.Port, SecureSocketOptions.SslOnConnect);
            await smtpClient.AuthenticateAsync(_emailSettings.UserName, _emailSettings.Password);
            await smtpClient.SendAsync(message);
        }
    }
}
