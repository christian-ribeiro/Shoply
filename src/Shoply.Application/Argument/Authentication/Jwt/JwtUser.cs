using Shoply.Arguments.Enum.Module.Registration;

namespace Shoply.Application.Argument.Authentication
{
    public class JwtUser(long id, string email, string name, EnumLanguage language)
    {
        public long Id { get; private set; } = id;
        public string Email { get; private set; } = email;
        public string Name { get; private set; } = name;
        public EnumLanguage Language { get; private set; } = language;
    }
}