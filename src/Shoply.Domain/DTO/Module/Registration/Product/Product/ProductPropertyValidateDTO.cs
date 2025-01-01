using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateProduct>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateProduct>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteProduct>? ListRepeatedInputIdentityDelete { get; private set; }
    public ProductDTO? OriginalProductDTO { get; private set; }

    public ProductPropertyValidateDTO ValidateCreate(List<InputCreateProduct>? listRepeatedInputCreate, ProductDTO? originalProductDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreate;
        OriginalProductDTO = originalProductDTO;
        return this;
    }

    public ProductPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateProduct>? listRepeatedInputIdentityUpdate, ProductDTO originalProductDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdate;
        OriginalProductDTO = originalProductDTO;
        return this;
    }


    public ProductPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteProduct>? listRepeatedInputIdentityDelete, ProductDTO originalProductDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalProductDTO = originalProductDTO;
        return this;
    }
}