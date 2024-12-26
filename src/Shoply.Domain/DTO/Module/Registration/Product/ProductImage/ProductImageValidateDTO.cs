using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductImageValidateDTO : ProductImagePropertyValidateDTO
{
    public InputCreateProductImage? InputCreateProductImage { get; private set; }
    public InputIdentityDeleteProductImage? InputIdentityDeleteProductImage { get; private set; }

    public ProductImageValidateDTO ValidateCreate(InputCreateProductImage? inputCreateProductImage, List<InputCreateProductImage>? listRepeatedInputCreateProductImage, ProductImageDTO originalProductImageDTO)
    {
        InputCreateProductImage = inputCreateProductImage;
        ValidateCreate(listRepeatedInputCreateProductImage, originalProductImageDTO);
        return this;
    }

    public ProductImageValidateDTO ValidateDelete(InputIdentityDeleteProductImage? inputIdentityDeleteProductImage, List<InputIdentityDeleteProductImage>? listRepeatedInputIdentityDeleteProductImage, ProductImageDTO originalProductImageDTO)
    {
        InputIdentityDeleteProductImage = inputIdentityDeleteProductImage;
        ValidateDelete(listRepeatedInputIdentityDeleteProductImage, originalProductImageDTO);
        return this;
    }
}