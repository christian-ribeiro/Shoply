using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class UserPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateUser>? ListRepeatedInputCreateUser { get; private set; }
    public List<InputIdentityUpdateUser>? ListRepeatedInputIdentityUpdateUser { get; private set; }
    public List<InputIdentityDeleteUser>? ListRepeatedInputIdentityDeleteUser { get; private set; }

    public UserDTO? OriginalUserDTO { get; private set; }

    public UserPropertyValidateDTO ValidateCreate(List<InputCreateUser>? listRepeatedInputCreateUser, UserDTO? originalUserDTO)
    {
        ListRepeatedInputCreateUser = listRepeatedInputCreateUser;
        OriginalUserDTO = originalUserDTO;
        return this;
    }

    public UserPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateUser>? listRepeatedInputIdentityUpdateUser, UserDTO originalUserDTO)
    {
        ListRepeatedInputIdentityUpdateUser = listRepeatedInputIdentityUpdateUser;
        OriginalUserDTO = originalUserDTO;
        return this;
    }


    public UserPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteUser>? listRepeatedInputIdentityDeleteUser, UserDTO originalUserDTO)
    {
        ListRepeatedInputIdentityDeleteUser = listRepeatedInputIdentityDeleteUser;
        OriginalUserDTO = originalUserDTO;
        return this;
    }

}