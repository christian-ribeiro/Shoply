using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class UserValidateDTO : UserPropertyValidateDTO
{
    public InputCreateUser? InputCreateUser { get; private set; }
    public InputIdentityUpdateUser? InputIdentityUpdateUser { get; private set; }
    public InputIdentityDeleteUser? InputIdentityDeleteUser { get; private set; }
    public InputAuthenticateUser? InputAuthenticateUser { get; private set; }
    public InputRefreshTokenUser? InputRefreshTokenUser { get; private set; }
    public InputSendEmailForgotPasswordUser? InputSendEmailForgotPasswordUser { get; private set; }
    public InputRedefinePasswordForgotPasswordUser? InputRedefinePasswordForgotPasswordUser { get; private set; }
    public InputRedefinePasswordUser? InputRedefinePasswordUser { get; private set; }

    public UserValidateDTO ValidateCreate(InputCreateUser? inputCreateUser, List<InputCreateUser>? listRepeatedInputCreateUser, UserDTO originalUserDTO)
    {
        InputCreateUser = inputCreateUser;
        ValidateCreate(listRepeatedInputCreateUser, originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateUpdate(InputIdentityUpdateUser? inputIdentityUpdateUser, List<InputIdentityUpdateUser>? listRepeatedInputIdentityUpdateUser, UserDTO originalUserDTO)
    {
        InputIdentityUpdateUser = inputIdentityUpdateUser;
        ValidateUpdate(listRepeatedInputIdentityUpdateUser, originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateDelete(InputIdentityDeleteUser? inputIdentityDeleteUser, List<InputIdentityDeleteUser>? listRepeatedInputIdentityDeleteUser, UserDTO originalUserDTO)
    {
        InputIdentityDeleteUser = inputIdentityDeleteUser;
        ValidateDelete(listRepeatedInputIdentityDeleteUser, originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateAuthenticate(InputAuthenticateUser? inputAuthenticateUser, UserDTO? originalUserDTO)
    {
        InputAuthenticateUser = inputAuthenticateUser;
        ValidateAuthenticate(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateRefreshToken(InputRefreshTokenUser? inputRefreshTokenUser, UserDTO? originalUserDTO)
    {
        InputRefreshTokenUser = inputRefreshTokenUser;
        ValidateRefreshToken(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateSendEmailForgotPassword(InputSendEmailForgotPasswordUser? inputSendEmailForgotPasswordUser, UserDTO? originalUserDTO)
    {
        InputSendEmailForgotPasswordUser = inputSendEmailForgotPasswordUser;
        ValidateSendEmailForgotPassword(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateRedefinePasswordForgotPassword(InputRedefinePasswordForgotPasswordUser? inputRedefinePasswordForgotPasswordUser, UserDTO? originalUserDTO)
    {
        InputRedefinePasswordForgotPasswordUser = inputRedefinePasswordForgotPasswordUser;
        ValidateRedefinePasswordForgotPassword(originalUserDTO);
        return this;
    }

    public UserValidateDTO ValidateRedefinePassword(InputRedefinePasswordUser? inputRedefinePasswordUser, UserDTO? originalUserDTO)
    {
        InputRedefinePasswordUser = inputRedefinePasswordUser;
        ValidateRedefinePassword(originalUserDTO);
        return this;
    }
}