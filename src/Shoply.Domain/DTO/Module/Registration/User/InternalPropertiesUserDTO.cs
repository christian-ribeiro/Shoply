using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class InternalPropertiesUserDTO : BaseInternalPropertiesDTO<InternalPropertiesUserDTO>
{
    public string? RefreshToken { get; private set; }
    public Guid? LoginKey { get; private set; }
    public string? PasswordRecoveryCode { get; private set; }

    public InternalPropertiesUserDTO() { }

    public InternalPropertiesUserDTO(string? refreshToken, Guid? loginKey, string? passwordRecoveryCode)
    {
        RefreshToken = refreshToken;
        LoginKey = loginKey;
        PasswordRecoveryCode = passwordRecoveryCode;
    }
}