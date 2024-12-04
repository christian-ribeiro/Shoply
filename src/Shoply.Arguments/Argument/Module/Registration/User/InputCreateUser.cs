using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateUser(string name, string password, string confirmPassword, string email) : BaseInputCreate<InputCreateUser>
{
    public string Name { get; private set; } = name;
    public string Password { get; private set; } = password;
    public string ConfirmPassword { get; private set; } = confirmPassword;
    public string Email { get; private set; } = email;
}