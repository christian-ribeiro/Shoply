namespace Shoply.Arguments.Argument.General.Authenticate
{
    public class InputAuthenticateUser(string email, string password)
    {
        public string Email { get; private set; } = email;
        public string Password { get; private set; } = password;
    }
}