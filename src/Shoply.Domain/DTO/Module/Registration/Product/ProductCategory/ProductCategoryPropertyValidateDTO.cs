using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductCategoryPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateProductCategory>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateProductCategory>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteProductCategory>? ListRepeatedInputIdentityDelete { get; private set; }
    public ProductCategoryDTO? OriginalProductCategoryDTO { get; private set; }

    public ProductCategoryPropertyValidateDTO ValidateCreate(List<InputCreateProductCategory>? listRepeatedInputCreate, ProductCategoryDTO? originalProductCategoryDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreate;
        OriginalProductCategoryDTO = originalProductCategoryDTO;
        return this;
    }

    public ProductCategoryPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateProductCategory>? listRepeatedInputIdentityUpdate, ProductCategoryDTO originalProductCategoryDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdate;
        OriginalProductCategoryDTO = originalProductCategoryDTO;
        return this;
    }


    public ProductCategoryPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteProductCategory>? listRepeatedInputIdentityDelete, ProductCategoryDTO originalProductCategoryDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalProductCategoryDTO = originalProductCategoryDTO;
        return this;
    }
}