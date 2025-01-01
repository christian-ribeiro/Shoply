using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductImagePropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateProductImage>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityDeleteProductImage>? ListRepeatedInputIdentityDelete { get; private set; }
    public ProductImageDTO? OriginalProductImageDTO { get; private set; }

    public ProductImagePropertyValidateDTO ValidateCreate(List<InputCreateProductImage>? listRepeatedInputCreate, ProductImageDTO? originalProductImageDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreate;
        OriginalProductImageDTO = originalProductImageDTO;
        return this;
    }

    public ProductImagePropertyValidateDTO ValidateDelete(List<InputIdentityDeleteProductImage>? listRepeatedInputIdentityDelete, ProductImageDTO originalProductImageDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalProductImageDTO = originalProductImageDTO;
        return this;
    }
}