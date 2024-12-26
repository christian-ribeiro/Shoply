using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IUserService : IBaseService<InputCreateUser, InputUpdateUser, InputIdentityUpdateUser, InputIdentityDeleteUser, InputIdentifierUser, OutputUser>
{
    Task<BaseResult<OutputAuthenticateUser>> Authenticate(InputAuthenticateUser inputAuthenticateUser);
    Task<BaseResult<OutputAuthenticateUser>> RefreshToken(InputRefreshTokenUser inputRefreshTokenUser);
    Task<BaseResult<bool>> SendEmailForgotPassword(InputSendEmailForgotPasswordUser inputSendEmailForgotPasswordUser);
    Task<BaseResult<bool>> RedefinePasswordForgotPassword(InputRedefinePasswordForgotPasswordUser inputRedefinePasswordForgotPasswordUser);
    Task<BaseResult<bool>> RedefinePassword(InputRedefinePasswordUser inputRedefinePasswordUser);
}