using Shoply.Application.Integration.Argument.Service;

namespace Shoply.Application.Integration.Interface.Service;

public interface ISendEmailService
{
    Task SendEmailAsync(string toEmail, string subject, string body, bool isBodyHtml, SmtpConfiguration? smtpConfiguration);
}