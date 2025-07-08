using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using Microsoft.Extensions.Options;
using SocialNetwork.Core.Application.Dtos.Email;
using SocialNetwork.Core.Domain.Settings;
using SocialNetwork.Core.Application.Interfaces.Services;

namespace SocialNetwork.Infrastructure.Shared.Services
{
    public class EmailService : IEmailService
    {
        private readonly MailSettings _mailSettings;

        public EmailService(IOptions<MailSettings> mailSettings) => _mailSettings = mailSettings.Value;

        public async Task SendAsync(EmailRequest request)
        {
            var email = new MimeMessage();
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.EmailFrom));
            email.To.Add(MailboxAddress.Parse(request.To));
            email.Subject = request.Subject;
            email.Body = new TextPart(TextFormat.Html) { Text = request.Body };

            using var smtp = new MailKit.Net.Smtp.SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
            await smtp.ConnectAsync(_mailSettings.SmtpHost, _mailSettings.SmtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.SmtpUser, _mailSettings.SmtpPass);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
