using Shoply.Arguments.Enum.Module.Registration;

namespace Shoply.Arguments.Argument.General.Session
{
    public class LoggedUser(long id, string name, string email, EnumLanguage language)
    {
        public long Id { get; private set; } = id;
        public string Name { get; private set; } = name;
        public string Email { get; private set; } = email;
        public EnumLanguage Language { get; private set; } = language;
    }
}