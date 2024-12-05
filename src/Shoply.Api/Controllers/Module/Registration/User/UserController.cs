using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoply.Api.Controllers.Base;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.UnitOfWork.Interface;
using System.Net;

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
            var result = await _service.Authenticate(inputAuthenticateUser);
            if (result.IsSuccess)
                return await ResponseAsync(result.Value);

            return await ResponseAsync(result.DetailedError?.Message, HttpStatusCode.BadRequest);
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
}