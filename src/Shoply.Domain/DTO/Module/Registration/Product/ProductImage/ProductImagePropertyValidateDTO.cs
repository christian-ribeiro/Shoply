using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductImagePropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateProductImage>? ListRepeatedInputCreateProductImage { get; private set; }
    public List<InputIdentityDeleteProductImage>? ListRepeatedInputIdentityDeleteProductImage { get; private set; }
    public ProductImageDTO? OriginalProductImageDTO { get; private set; }

    public ProductImagePropertyValidateDTO ValidateCreate(List<InputCreateProductImage>? listRepeatedInputCreateProductImage, ProductImageDTO? originalProductImageDTO)
    {
        ListRepeatedInputCreateProductImage = listRepeatedInputCreateProductImage;
        OriginalProductImageDTO = originalProductImageDTO;
        return this;
    }

    public ProductImagePropertyValidateDTO ValidateDelete(List<InputIdentityDeleteProductImage>? listRepeatedInputIdentityDeleteProductImage, ProductImageDTO originalProductImageDTO)
    {
        ListRepeatedInputIdentityDeleteProductImage = listRepeatedInputIdentityDeleteProductImage;
        OriginalProductImageDTO = originalProductImageDTO;
        return this;
    }
}