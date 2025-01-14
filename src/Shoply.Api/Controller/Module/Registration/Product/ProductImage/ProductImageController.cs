using Microsoft.AspNetCore.Mvc;
using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class ProductImageController(IProductImageService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<IProductImageService, IShoplyUnitOfWork, InputCreateProductImage, InputIdentityDeleteProductImage, InputIdentifierProductImage, OutputProductImage>(service, unitOfWork, userService)
{
    [HttpPost("Create")]
    public override async Task<ActionResult<OutputProductImage>> Create([FromForm] InputCreateProductImage inputCreate)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.Create(inputCreate)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("Create/Multiple")]
    public override Task<ActionResult<OutputProductImage>> Create([FromForm] List<InputCreateProductImage> listInputCreate)
    {
        return base.Create(listInputCreate);
    }
}