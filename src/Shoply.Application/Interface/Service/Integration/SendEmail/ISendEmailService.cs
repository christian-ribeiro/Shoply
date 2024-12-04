using Shoply.Application.Argument.Integration;

namespace Shoply.Application.Interface.Service.Integration;

public interface ISendEmailService
{
    Task<bool> SendEmailAsync(string toEmail, string subject, string body, bool isBodyHtml, SmtpConfiguration? smtpConfiguration);
}