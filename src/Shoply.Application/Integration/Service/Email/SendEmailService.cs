using Microsoft.Extensions.Options;
using Shoply.Application.Integration.Argument.Service;
using Shoply.Application.Integration.Interface.Service;
using System.Net;
using System.Net.Mail;

namespace Shoply.Application.Integration.Service;

public class SendEmailService(IOptions<SmtpConfiguration> options) : ISendEmailService
{
    public readonly SmtpConfiguration _smtpConfiguration = options.Value;

    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHtml, SmtpConfiguration? smtpConfiguration)
    {
        _ = smtpConfiguration ??= _smtpConfiguration;

        using var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(smtpConfiguration!.From, smtpConfiguration!.DisplayName);
        mailMessage.To.Add(new MailAddress(toEmail));
        mailMessage.Subject = subject;
        mailMessage.Body = body;
        mailMessage.IsBodyHtml = isBodyHtml;

        if (!string.IsNullOrEmpty(smtpConfiguration.EmailCopy))
            mailMessage.CC.Add(new MailAddress(smtpConfiguration.EmailCopy));

        using var smtpClient = new SmtpClient(smtpConfiguration.Server, smtpConfiguration.Port)
        {
            Credentials = new NetworkCredential(smtpConfiguration.Username, smtpConfiguration.Password),
            EnableSsl = smtpConfiguration.Ssl
        };

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
        }
        catch (Exception)
        {
            throw new InvalidOperationException();
        }
    }
}