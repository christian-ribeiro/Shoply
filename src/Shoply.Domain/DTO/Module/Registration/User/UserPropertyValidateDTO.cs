using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class UserPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateUser>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateUser>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteUser>? ListRepeatedInputIdentityDelete { get; private set; }
    public UserDTO? OriginalUserDTO { get; private set; }

    public UserPropertyValidateDTO ValidateCreate(List<InputCreateUser>? listRepeatedInputCreateUser, UserDTO? originalUserDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreateUser;
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateUser>? listRepeatedInputIdentityUpdateUser, UserDTO originalUserDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdateUser;
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteUser>? listRepeatedInputIdentityDeleteUser, UserDTO originalUserDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDeleteUser;
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateAuthenticate(UserDTO? originalUserDTO)
    {
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateRefreshToken(UserDTO? originalUserDTO)
    {
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateSendEmailForgotPassword(UserDTO? originalUserDTO)
    {
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateRedefinePasswordForgotPassword(UserDTO? originalUserDTO)
    {
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateRedefinePassword(UserDTO? originalUserDTO)
    {
        OriginalUserDTO = originalUserDTO;
        return this;
    }
}