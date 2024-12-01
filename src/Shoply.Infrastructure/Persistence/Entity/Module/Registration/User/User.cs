using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.Entity.Module.Registration;

public class User(string name, string password, string email, EnumLanguage language, string? refreshToken, Guid? loginKey, string? passwordRecoveryCode) : BaseEntity<User, InputCreateUser, InputUpdateUser, OutputUser, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO>
{
    public string Name { get; private set; } = name;
    public string Password { get; private set; } = password;
    public string Email { get; private set; } = email;
    public EnumLanguage Language { get; private set; } = language;
    public string? RefreshToken { get; private set; } = refreshToken;
    public Guid? LoginKey { get; private set; } = loginKey;
    public string? PasswordRecoveryCode { get; private set; } = passwordRecoveryCode;

    #region Virtual Properties
    #region External
    #region User
    public virtual List<User>? ListCreationUserUser { get; private set; }
    public virtual List<User>? ListChangeUserUser { get; private set; }
    #endregion
    #endregion
    #endregion
}