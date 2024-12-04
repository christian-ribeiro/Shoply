using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.CustomAttribute.Validate;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateUser(string name, string password, string confirmPassword, string email) : BaseInputCreate<InputCreateUser>
{
    [Required]
    public string Name { get; private set; } = name;
    [Required]
    public string Password { get; private set; } = password;
    [PasswordMatch("Password", ErrorMessage = "As senhas não coincidem.")]
    public string ConfirmPassword { get; private set; } = confirmPassword;
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
    public string Email { get; private set; } = email;
}