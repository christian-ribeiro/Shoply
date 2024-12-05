namespace Shoply.Arguments.Argument.General.Authenticate
{
    public class OutputAuthenticateUser(long id, string token)
    {
        public long Id { get; private set; } = id;
        public string Token { get; private set; } = token;
    }
}