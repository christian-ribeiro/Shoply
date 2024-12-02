namespace Shoply.Application.Integration.Argument.Service;

public class SmtpConfiguration
{
    public string Server { get; set; } = String.Empty;
    public int Port { get; set; }
    public string DisplayName { get; set; } = String.Empty;
    public string From { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public bool Ssl { get; set; }
    public string? EmailCopy { get; set; }
}