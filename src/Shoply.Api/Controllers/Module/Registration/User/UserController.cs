using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoply.Api.Controllers.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.UnitOfWork.Interface;

namespace Shoply.Api.Controllers.Module.Registration;

public class UserController(IUserService service, IShoplyUnitOfWork unitOfWork) : BaseController<IUserService, IShoplyUnitOfWork, OutputUser, InputIdentifierUser, InputCreateUser, InputUpdateUser, InputIdentityUpdateUser, InputIdentityDeleteUser>(service, unitOfWork, service)
{
    [AllowAnonymous]
    [HttpPost("Authenticate")]
    public async Task<ActionResult<string>> Authenticate([FromBody] InputAuthenticateUser inputAuthenticateUser)
    {
        try
        {
            var token = await _service.Authenticate(inputAuthenticateUser);
            return Ok(token);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.ToString());
        }
    }
}