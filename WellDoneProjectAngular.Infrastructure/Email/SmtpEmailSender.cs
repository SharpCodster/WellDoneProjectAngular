using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using WellDoneProjectAngular.Core.Models;

namespace WellDoneProjectAngular.Infrastructure.Email
{
    internal class SmtpEmailSender : IEmailSender
    {
        private readonly IOptions<EmailSettings> _optionsEmailSettings;

        public SmtpEmailSender(IOptions<EmailSettings> optionsEmailSettings)
        {
            _optionsEmailSettings = optionsEmailSettings;
        }

        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            SmtpClient Client = new SmtpClient()
            {
                Host = _optionsEmailSettings.Value.SmtpSettings.Host,
                Port = _optionsEmailSettings.Value.SmtpSettings.Port,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential()
                {
                    UserName = _optionsEmailSettings.Value.SmtpSettings.UserName,
                    Password = _optionsEmailSettings.Value.SmtpSettings.Password,
                    Domain = _optionsEmailSettings.Value.SmtpSettings.Domain
                }
            };

            MailAddress FromeMail = new MailAddress(_optionsEmailSettings.Value.SenderEmailAddress, _optionsEmailSettings.Value.SenderEmailFullname);
            MailAddress ToeMail = new MailAddress(email);

            MailMessage Message = new MailMessage()
            {
                From = FromeMail,
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            Message.To.Add(ToeMail);

            Client.Send(Message);

            await Task.CompletedTask;
        }
    }
}
