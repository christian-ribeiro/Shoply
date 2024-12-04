using System.ComponentModel.DataAnnotations;

namespace Shoply.Arguments.Argument.General.Authenticate
{
    public class InputAuthenticateUser(string email, string password)
    {
        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido.")]
        public string Email { get; private set; } = email;
        [Required(ErrorMessage = "A senha é obrigatória.")]
        public string Password { get; private set; } = password;
    }
}