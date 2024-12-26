using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductCategoryValidateDTO : ProductCategoryPropertyValidateDTO
{
    public InputCreateProductCategory? InputCreateProductCategory { get; private set; }
    public InputIdentityUpdateProductCategory? InputIdentityUpdateProductCategory { get; private set; }
    public InputIdentityDeleteProductCategory? InputIdentityDeleteProductCategory { get; private set; }

    public ProductCategoryValidateDTO ValidateCreate(InputCreateProductCategory? inputCreateProductCategory, List<InputCreateProductCategory>? listRepeatedInputCreateProductCategory, ProductCategoryDTO originalProductCategoryDTO)
    {
        InputCreateProductCategory = inputCreateProductCategory;
        ValidateCreate(listRepeatedInputCreateProductCategory, originalProductCategoryDTO);
        return this;
    }

    public ProductCategoryValidateDTO ValidateUpdate(InputIdentityUpdateProductCategory? inputIdentityUpdateProductCategory, List<InputIdentityUpdateProductCategory>? listRepeatedInputIdentityUpdateProductCategory, ProductCategoryDTO originalProductCategoryDTO)
    {
        InputIdentityUpdateProductCategory = inputIdentityUpdateProductCategory;
        ValidateUpdate(listRepeatedInputIdentityUpdateProductCategory, originalProductCategoryDTO);
        return this;
    }

    public ProductCategoryValidateDTO ValidateDelete(InputIdentityDeleteProductCategory? inputIdentityDeleteProductCategory, List<InputIdentityDeleteProductCategory>? listRepeatedInputIdentityDeleteProductCategory, ProductCategoryDTO originalProductCategoryDTO)
    {
        InputIdentityDeleteProductCategory = inputIdentityDeleteProductCategory;
        ValidateDelete(listRepeatedInputIdentityDeleteProductCategory, originalProductCategoryDTO);
        return this;
    }
}