using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module.Registration;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputUser(string name, string password, string email, EnumLanguage language, string? refreshToken, Guid? loginKey, string? passwordRecoveryCode) : BaseOutput<OutputUser>
{
    public string Name { get; set; } = name;
    public string Password { get; set; } = password;
    public string Email { get; set; } = email;
    public EnumLanguage Language { get; set; } = language;
    public string? RefreshToken { get; set; } = refreshToken;
    public Guid? LoginKey { get; set; } = loginKey;
    public string? PasswordRecoveryCode { get; set; } = passwordRecoveryCode;
}