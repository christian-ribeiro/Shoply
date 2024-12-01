using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesUserDTO(string name, string password, string email, EnumLanguage language) : BaseExternalPropertiesDTO<ExternalPropertiesUserDTO>
{
    public string Name { get; private set; } = name;
    public string Password { get; private set; } = password;
    public string Email { get; private set; } = email;
    public EnumLanguage Language { get; private set; } = language;
}