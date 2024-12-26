using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateProduct>? ListRepeatedInputCreateProduct { get; private set; }
    public List<InputIdentityUpdateProduct>? ListRepeatedInputIdentityUpdateProduct { get; private set; }
    public List<InputIdentityDeleteProduct>? ListRepeatedInputIdentityDeleteProduct { get; private set; }
    public ProductDTO? OriginalProductDTO { get; private set; }

    public ProductPropertyValidateDTO ValidateCreate(List<InputCreateProduct>? listRepeatedInputCreateProduct, ProductDTO? originalProductDTO)
    {
        ListRepeatedInputCreateProduct = listRepeatedInputCreateProduct;
        OriginalProductDTO = originalProductDTO;
        return this;
    }

    public ProductPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateProduct>? listRepeatedInputIdentityUpdateProduct, ProductDTO originalProductDTO)
    {
        ListRepeatedInputIdentityUpdateProduct = listRepeatedInputIdentityUpdateProduct;
        OriginalProductDTO = originalProductDTO;
        return this;
    }


    public ProductPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteProduct>? listRepeatedInputIdentityDeleteProduct, ProductDTO originalProductDTO)
    {
        ListRepeatedInputIdentityDeleteProduct = listRepeatedInputIdentityDeleteProduct;
        OriginalProductDTO = originalProductDTO;
        return this;
    }
}