using Microsoft.Extensions.Options;
using Polly;
using Shoply.Application.Argument.Integration;
using Shoply.Application.Interface.Service.Integration;
using System.Net;
using System.Net.Mail;

namespace Shoply.Application.Service.Integration;

public class SendEmailService(IOptions<SmtpConfiguration> options) : ISendEmailService
{
    public readonly SmtpConfiguration _smtpConfiguration = options.Value;

    public async Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isBodyHtml, SmtpConfiguration? smtpConfiguration)
    {
        smtpConfiguration ??= _smtpConfiguration;

        using var mailMessage = CreateMailMessage(toEmail, subject, body, isBodyHtml, smtpConfiguration);
        using var smtpClient = new SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port)
        {
            Credentials = new NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password),
            EnableSsl = smtpConfiguration.Ssl
        };

        var retryPolicy = Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        return await Policy<bool>
            .Handle<Exception>()
            .FallbackAsync(false)
            .WrapAsync(retryPolicy)
            .ExecuteAsync(async () =>
            {
                await smtpClient.SendMailAsync(mailMessage);
                return true;
            });
    }

    private static MailMessage CreateMailMessage(string toEmail, string subject, string body, bool isBodyHtml, SmtpConfiguration smtpConfiguration)
    {
        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpConfiguration.From, smtpConfiguration.DisplayName),
            Subject = subject,
            Body = body,
            IsBodyHtml = isBodyHtml
        };

        mailMessage.To.Add(new MailAddress(toEmail));

        if (!string.IsNullOrEmpty(smtpConfiguration.EmailCopy))
            mailMessage.CC.Add(new MailAddress(smtpConfiguration.EmailCopy));

        return mailMessage;
    }
}