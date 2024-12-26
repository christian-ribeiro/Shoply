using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductCategoryPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateProductCategory>? ListRepeatedInputCreateProductCategory { get; private set; }
    public List<InputIdentityUpdateProductCategory>? ListRepeatedInputIdentityUpdateProductCategory { get; private set; }
    public List<InputIdentityDeleteProductCategory>? ListRepeatedInputIdentityDeleteProductCategory { get; private set; }
    public ProductCategoryDTO? OriginalProductCategoryDTO { get; private set; }

    public ProductCategoryPropertyValidateDTO ValidateCreate(List<InputCreateProductCategory>? listRepeatedInputCreateProductCategory, ProductCategoryDTO? originalProductCategoryDTO)
    {
        ListRepeatedInputCreateProductCategory = listRepeatedInputCreateProductCategory;
        OriginalProductCategoryDTO = originalProductCategoryDTO;
        return this;
    }

    public ProductCategoryPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateProductCategory>? listRepeatedInputIdentityUpdateProductCategory, ProductCategoryDTO originalProductCategoryDTO)
    {
        ListRepeatedInputIdentityUpdateProductCategory = listRepeatedInputIdentityUpdateProductCategory;
        OriginalProductCategoryDTO = originalProductCategoryDTO;
        return this;
    }


    public ProductCategoryPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteProductCategory>? listRepeatedInputIdentityDeleteProductCategory, ProductCategoryDTO originalProductCategoryDTO)
    {
        ListRepeatedInputIdentityDeleteProductCategory = listRepeatedInputIdentityDeleteProductCategory;
        OriginalProductCategoryDTO = originalProductCategoryDTO;
        return this;
    }
}