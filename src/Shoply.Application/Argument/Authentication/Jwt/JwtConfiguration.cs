namespace Shoply.Application.Argument.Authentication
{
    public class JwtConfiguration
    {
        public string Key { get; set; } = String.Empty;
        public string Issuer { get; set; } = String.Empty;
        public string Audience { get; set; } = String.Empty;
    }
}