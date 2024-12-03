using Microsoft.Extensions.Options;
using Shoply.Application.Integration.Argument.Service;
using Shoply.Application.Integration.Interface.Service;
using System.Net;
using System.Net.Mail;

namespace Shoply.Application.Integration.Service;

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

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            return true;
        }
        catch (Exception)
        {
            return false;
        }
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