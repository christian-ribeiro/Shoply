using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputRefreshTokenUser(string token, string refreshToken)
{
    public string Token { get; private set; } = token;
    public string RefreshToken { get; private set; } = refreshToken;
}