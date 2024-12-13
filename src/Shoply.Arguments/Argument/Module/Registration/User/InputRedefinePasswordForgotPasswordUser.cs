using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputRedefinePasswordForgotPasswordUser(string passwordRecoveryCode, string newPassword, string confirmNewPassword)
{
    public string PasswordRecoveryCode { get; private set; } = passwordRecoveryCode;
    public string NewPassword { get; private set; } = newPassword;
    public string ConfirmNewPassword { get; private set; } = confirmNewPassword;
}