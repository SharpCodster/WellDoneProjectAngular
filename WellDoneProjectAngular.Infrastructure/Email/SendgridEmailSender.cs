using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Infrastructure.Email
{
    public class SendgridEmailSender : IEmailSender
    {
        private readonly IOptions<EmailSettings> _optionsEmailSettings;

        public SendgridEmailSender(IOptions<EmailSettings> optionsEmailSettings)
        {
            _optionsEmailSettings = optionsEmailSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var client = new SendGridClient(_optionsEmailSettings.Value.SendGridSettings.SendGridApiKey);
            var msg = new SendGridMessage();
            msg.SetFrom(new EmailAddress(_optionsEmailSettings.Value.SenderEmailAddress, _optionsEmailSettings.Value.SenderEmailFullname));

            msg.AddTo(new EmailAddress(email));

            msg.SetSubject(subject);
            msg.AddContent(MimeType.Html, htmlMessage);

            var response = await client.SendEmailAsync(msg);
        }
    }
}
