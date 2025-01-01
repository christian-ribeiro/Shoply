using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductValidateDTO : ProductPropertyValidateDTO
{
    public InputCreateProduct? InputCreate { get; private set; }
    public InputIdentityUpdateProduct? InputIdentityUpdate { get; private set; }
    public InputIdentityDeleteProduct? InputIdentityDelete { get; private set; }

    public ProductValidateDTO ValidateCreate(InputCreateProduct? inputCreate, List<InputCreateProduct>? listRepeatedInputCreate, ProductDTO originalProductDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(listRepeatedInputCreate, originalProductDTO);
        return this;
    }

    public ProductValidateDTO ValidateUpdate(InputIdentityUpdateProduct? inputIdentityUpdate, List<InputIdentityUpdateProduct>? listRepeatedInputIdentityUpdate, ProductDTO originalProductDTO)
    {
        InputIdentityUpdate = inputIdentityUpdate;
        ValidateUpdate(listRepeatedInputIdentityUpdate, originalProductDTO);
        return this;
    }

    public ProductValidateDTO ValidateDelete(InputIdentityDeleteProduct? inputIdentityDelete, List<InputIdentityDeleteProduct>? listRepeatedInputIdentityDelete, ProductDTO originalProductDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalProductDTO);
        return this;
    }
}