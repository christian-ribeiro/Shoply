using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shoply.Api.Controllers.Base;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.UnitOfWork.Interface;

namespace Shoply.Api.Controllers.Module.Registration;

public class CustomerAddressController(ICustomerAddressService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<ICustomerAddressService, IShoplyUnitOfWork, OutputCustomerAddress, InputIdentifierCustomerAddress, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentityUpdateCustomerAddress, InputIdentityDeleteCustomerAddress>(service, unitOfWork, userService)
{
    [AllowAnonymous]
    [HttpPost("GetDynamic")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType<BaseResponseError>(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<List<OutputCustomerAddress>>> GetDynamic([FromBody] string[] fields)
    {
        try
        {
            return await ResponseAsync(await _service.GetDynamic(fields));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }
}
