using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoply.Api.Controllers.Base;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controllers.Module.Registration;

public class UserController(IUserService service, IShoplyUnitOfWork unitOfWork) : BaseController<IUserService, IShoplyUnitOfWork, OutputUser, InputIdentifierUser, InputCreateUser, InputUpdateUser, InputIdentityUpdateUser, InputIdentityDeleteUser>(service, unitOfWork, service)
{
    [AllowAnonymous]
    [HttpPost("Authenticate")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OutputAuthenticateUser>> Authenticate([FromBody] InputAuthenticateUser inputAuthenticateUser)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service.Authenticate(inputAuthenticateUser)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [AllowAnonymous]
    [HttpPost("RefreshToken")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OutputAuthenticateUser>> RefreshToken([FromBody] InputRefreshTokenUser inputRefreshTokenUser)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service.RefreshToken(inputRefreshTokenUser)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [AllowAnonymous]
    [HttpPost("SendEmailForgotPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OutputAuthenticateUser>> SendEmailForgotPassword([FromBody] InputSendEmailForgotPasswordUser inputSendEmailForgotPasswordUser)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service.SendEmailForgotPassword(inputSendEmailForgotPasswordUser)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [AllowAnonymous]
    [HttpPut("RedefinePasswordForgotPassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OutputAuthenticateUser>> RedefinePasswordForgotPassword([FromBody] InputRedefinePasswordForgotPasswordUser inputRedefinePasswordForgotPasswordUser)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service.RedefinePasswordForgotPassword(inputRedefinePasswordForgotPasswordUser)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPut("RedefinePassword")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<OutputAuthenticateUser>> RedefinePassword([FromBody] InputRedefinePasswordUser inputRedefinePasswordUser)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service.RedefinePassword(inputRedefinePasswordUser)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
}