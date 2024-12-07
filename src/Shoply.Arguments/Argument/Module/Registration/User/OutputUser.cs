using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module.Registration;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputUser(string name, string password, string email, EnumLanguage language, string? refreshToken, Guid? loginKey, string? passwordRecoveryCode) : BaseOutput<OutputUser>
{
    public string Name { get; private set; } = name;
    public string Password { get; private set; } = password;
    public string Email { get; private set; } = email;
    public EnumLanguage Language { get; private set; } = language;
    public string? RefreshToken { get; private set; } = refreshToken;
    public Guid? LoginKey { get; private set; } = loginKey;
    public string? PasswordRecoveryCode { get; private set; } = passwordRecoveryCode;
}