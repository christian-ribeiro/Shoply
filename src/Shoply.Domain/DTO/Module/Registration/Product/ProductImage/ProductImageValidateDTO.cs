using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductImageValidateDTO : ProductImagePropertyValidateDTO
{
    public InputCreateProductImage? InputCreate { get; private set; }
    public InputIdentityDeleteProductImage? InputIdentityDelete { get; private set; }

    public ProductImageValidateDTO ValidateCreate(InputCreateProductImage? inputCreate, List<InputCreateProductImage>? listRepeatedInputCreate, ProductImageDTO originalProductImageDTO, ProductDTO? relatedProductDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(listRepeatedInputCreate, originalProductImageDTO, relatedProductDTO);
        return this;
    }

    public ProductImageValidateDTO ValidateDelete(InputIdentityDeleteProductImage? inputIdentityDelete, List<InputIdentityDeleteProductImage>? listRepeatedInputIdentityDelete, ProductImageDTO originalProductImageDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalProductImageDTO);
        return this;
    }
}