using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductValidateDTO : ProductPropertyValidateDTO
{
    public InputCreateProduct? InputCreateProduct { get; private set; }
    public InputIdentityUpdateProduct? InputIdentityUpdateProduct { get; private set; }
    public InputIdentityDeleteProduct? InputIdentityDeleteProduct { get; private set; }

    public ProductValidateDTO ValidateCreate(InputCreateProduct? inputCreateProduct, List<InputCreateProduct>? listRepeatedInputCreateProduct, ProductDTO originalProductDTO)
    {
        InputCreateProduct = inputCreateProduct;
        ValidateCreate(listRepeatedInputCreateProduct, originalProductDTO);
        return this;
    }

    public ProductValidateDTO ValidateUpdate(InputIdentityUpdateProduct? inputIdentityUpdateProduct, List<InputIdentityUpdateProduct>? listRepeatedInputIdentityUpdateProduct, ProductDTO originalProductDTO)
    {
        InputIdentityUpdateProduct = inputIdentityUpdateProduct;
        ValidateUpdate(listRepeatedInputIdentityUpdateProduct, originalProductDTO);
        return this;
    }

    public ProductValidateDTO ValidateDelete(InputIdentityDeleteProduct? inputIdentityDeleteProduct, List<InputIdentityDeleteProduct>? listRepeatedInputIdentityDeleteProduct, ProductDTO originalProductDTO)
    {
        InputIdentityDeleteProduct = inputIdentityDeleteProduct;
        ValidateDelete(listRepeatedInputIdentityDeleteProduct, originalProductDTO);
        return this;
    }
}