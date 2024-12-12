using Shoply.Arguments.Enum.Module.Registration;

namespace Shoply.Application.Argument.Authentication
{
    public class JwtUser(long id, string email, string name, Guid loginKey, EnumLanguage language)
    {
        public long Id { get; private set; } = id;
        public string Email { get; private set; } = email;
        public string Name { get; private set; } = name;
        public Guid LoginKey { get; private set; } = loginKey;
        public EnumLanguage Language { get; private set; } = language;
    }
}