using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class UserValidateDTO : UserPropertyValidateDTO
{
    public InputCreateUser? InputCreate { get; private set; }
    public InputIdentityUpdateUser? InputIdentityUpdate { get; private set; }
    public InputIdentityDeleteUser? InputIdentityDelete { get; private set; }
    public InputAuthenticateUser? InputAuthenticate { get; private set; }
    public InputRefreshTokenUser? InputRefreshToken { get; private set; }
    public InputSendEmailForgotPasswordUser? InputSendEmailForgotPassword { get; private set; }
    public InputRedefinePasswordForgotPasswordUser? InputRedefinePasswordForgotPassword { get; private set; }
    public InputRedefinePasswordUser? InputRedefinePassword { get; private set; }

    public UserValidateDTO ValidateCreate(InputCreateUser? inputCreate, List<InputCreateUser>? listRepeatedInputCreate, UserDTO originalUserDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(listRepeatedInputCreate, originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateUpdate(InputIdentityUpdateUser? inputIdentityUpdate, List<InputIdentityUpdateUser>? listRepeatedInputIdentityUpdate, UserDTO originalUserDTO)
    {
        InputIdentityUpdate = inputIdentityUpdate;
        ValidateUpdate(listRepeatedInputIdentityUpdate, originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateDelete(InputIdentityDeleteUser? inputIdentityDelete, List<InputIdentityDeleteUser>? listRepeatedInputIdentityDelete, UserDTO originalUserDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateAuthenticate(InputAuthenticateUser? inputAuthenticate, UserDTO? originalUserDTO)
    {
        InputAuthenticate = inputAuthenticate;
        ValidateAuthenticate(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateRefreshToken(InputRefreshTokenUser? inputRefreshToken, UserDTO? originalUserDTO)
    {
        InputRefreshToken = inputRefreshToken;
        ValidateRefreshToken(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateSendEmailForgotPassword(InputSendEmailForgotPasswordUser? inputSendEmailForgotPassword, UserDTO? originalUserDTO)
    {
        InputSendEmailForgotPassword = inputSendEmailForgotPassword;
        ValidateSendEmailForgotPassword(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateRedefinePasswordForgotPassword(InputRedefinePasswordForgotPasswordUser? inputRedefinePasswordForgotPassword, UserDTO? originalUserDTO)
    {
        InputRedefinePasswordForgotPassword = inputRedefinePasswordForgotPassword;
        ValidateRedefinePasswordForgotPassword(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateRedefinePassword(InputRedefinePasswordUser? inputRedefinePassword, UserDTO? originalUserDTO)
    {
        InputRedefinePassword = inputRedefinePassword;
        ValidateRedefinePassword(originalUserDTO);
        return this;
    }
}