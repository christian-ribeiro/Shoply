using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class InternalPropertiesUserDTO(string? refreshToken, Guid? loginKey, string? passwordRecoveryCode) : BaseInternalPropertiesDTO<InternalPropertiesUserDTO>
{
    public string? RefreshToken { get; private set; } = refreshToken;
    public Guid? LoginKey { get; private set; } = loginKey;
    public string? PasswordRecoveryCode { get; private set; } = passwordRecoveryCode;
}