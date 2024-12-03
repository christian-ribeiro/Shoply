using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesUserDTO : BaseExternalPropertiesDTO<ExternalPropertiesUserDTO>
{
    public string Name { get; private set; }
    public string Password { get; private set; }
    public string Email { get; private set; }
    public EnumLanguage Language { get; private set; }

    public ExternalPropertiesUserDTO() { }

    public ExternalPropertiesUserDTO(string name, string password, string email, EnumLanguage language)
    {
        Name = name;
        Password = password;
        Email = email;
        Language = language;
    }
}