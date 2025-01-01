using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductCategoryValidateDTO : ProductCategoryPropertyValidateDTO
{
    public InputCreateProductCategory? InputCreate { get; private set; }
    public InputIdentityUpdateProductCategory? InputIdentityUpdate { get; private set; }
    public InputIdentityDeleteProductCategory? InputIdentityDelete { get; private set; }

    public ProductCategoryValidateDTO ValidateCreate(InputCreateProductCategory? inputCreate, List<InputCreateProductCategory>? listRepeatedInputCreate, ProductCategoryDTO originalProductCategoryDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(listRepeatedInputCreate, originalProductCategoryDTO);
        return this;
    }

    public ProductCategoryValidateDTO ValidateUpdate(InputIdentityUpdateProductCategory? inputIdentityUpdate, List<InputIdentityUpdateProductCategory>? listRepeatedInputIdentityUpdate, ProductCategoryDTO originalProductCategoryDTO)
    {
        InputIdentityUpdate = inputIdentityUpdate;
        ValidateUpdate(listRepeatedInputIdentityUpdate, originalProductCategoryDTO);
        return this;
    }

    public ProductCategoryValidateDTO ValidateDelete(InputIdentityDeleteProductCategory? inputIdentityDelete, List<InputIdentityDeleteProductCategory>? listRepeatedInputIdentityDelete, ProductCategoryDTO originalProductCategoryDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalProductCategoryDTO);
        return this;
    }
}