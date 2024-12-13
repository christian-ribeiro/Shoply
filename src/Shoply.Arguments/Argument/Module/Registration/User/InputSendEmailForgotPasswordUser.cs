using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputSendEmailForgotPasswordUser(string email)
{
    public string Email { get; private set; } = email;
}