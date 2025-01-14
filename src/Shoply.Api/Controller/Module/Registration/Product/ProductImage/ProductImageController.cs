using Microsoft.AspNetCore.Mvc;
using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class ProductImageController(IProductImageService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<IProductImageService, IShoplyUnitOfWork, InputCreateProductImage, InputIdentityDeleteProductImage, InputIdentifierProductImage, OutputProductImage>(service, unitOfWork, userService)
{
    [HttpPost("Create/{productId}")]
    public async Task<ActionResult<OutputProductImage>> Create(IFormFile file, [FromRoute] long productId)
    {
        try
        {
            return await ResponseAsync(PrepareReturn(await _service!.Create(new InputCreateProductImage(file.FileName, file.ContentType, file.Length, ConvertFileToByteArray(file), productId))));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [HttpPost("Create/Multiple/{productId}")]
    public async Task<ActionResult<OutputProductImage>> Create(List<IFormFile> listFile, [FromRoute] long productId)
    {
        try
        {
            var listInputCreateProductImage = (from i in listFile select new InputCreateProductImage(i.FileName, i.ContentType, i.Length, ConvertFileToByteArray(i), productId)).ToList();

            return await ResponseAsync(PrepareReturn(await _service!.Create(listInputCreateProductImage)));
        }
        catch (Exception ex)
        {
            return await ResponseExceptionAsync(ex);
        }
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<OutputProductImage>> Create([FromBody] InputCreateProductImage inputCreate)
    {
        throw new NotImplementedException();
    }

    [ApiExplorerSettings(IgnoreApi = true)]
    public override Task<ActionResult<OutputProductImage>> Create([FromBody] List<InputCreateProductImage> listInputCreate)
    {
        throw new NotImplementedException();
    }
}