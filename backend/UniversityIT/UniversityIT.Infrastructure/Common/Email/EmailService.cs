using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Application.ValueObjects;

namespace UniversityIT.Infrastructure.Common.Email
{
    public class EmailService : IMessageService
    {
        private readonly EMailOptions _eMailOptions;

        public EmailService(IOptions<EMailOptions> options)
        {
            _eMailOptions = options.Value;
        }

        public async Task<bool> SendMessage(MessageReceiver receiver, string subject, string message)
        {
            bool success = false;

            try
            {
                using (var smtp = new SmtpClient())
                {
                    await smtp.ConnectAsync(_eMailOptions.YandexHost, _eMailOptions.YandexPort, _eMailOptions.YandexSsl);
                    await smtp.AuthenticateAsync(_eMailOptions.YandexLogin, _eMailOptions.YandexPass);

                    var msg = new MimeMessage()
                    {
                        Subject = subject,
                        Body = new TextPart(MimeKit.Text.TextFormat.Html)
                        {
                            Text = message
                        }
                    };

                    MailboxAddress address = new MailboxAddress("", receiver.StringAddress);
                    msg.To.Add(address);
                    msg.From.Add(new MailboxAddress("AmGPGU-IT", _eMailOptions.YandexLogin));

                    await smtp.SendAsync(msg);
                    await smtp.DisconnectAsync(true);

                    success = true;
                }
            }
            catch (Exception ex)
            {
                success = false;
                Console.WriteLine(ex);
            }

            return success;
        }
    }
}