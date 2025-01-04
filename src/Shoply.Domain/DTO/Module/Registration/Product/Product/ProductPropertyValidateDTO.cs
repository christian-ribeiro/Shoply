using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateProduct>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateProduct>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteProduct>? ListRepeatedInputIdentityDelete { get; private set; }
    public ProductDTO? OriginalProductDTO { get; private set; }
    public ProductCategoryDTO? RelatedProductCategoryDTO { get; private set; }
    public MeasureUnitDTO? RelatedMeasureUnitDTO { get; private set; }
    public BrandDTO? RelatedBrandDTO { get; private set; }

    public ProductPropertyValidateDTO ValidateCreate(List<InputCreateProduct>? listRepeatedInputCreate, ProductDTO? originalProductDTO, ProductCategoryDTO relatedProductCategoryDTO, MeasureUnitDTO relatedMeasureUnitDTO, BrandDTO relatedBrandDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreate;
        OriginalProductDTO = originalProductDTO;
        RelatedProductCategoryDTO = relatedProductCategoryDTO;
        RelatedMeasureUnitDTO = relatedMeasureUnitDTO;
        RelatedBrandDTO = relatedBrandDTO;
        return this;
    }

    public ProductPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateProduct>? listRepeatedInputIdentityUpdate, ProductDTO originalProductDTO, ProductCategoryDTO relatedProductCategoryDTO, MeasureUnitDTO relatedMeasureUnitDTO, BrandDTO relatedBrandDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdate;
        OriginalProductDTO = originalProductDTO;
        RelatedProductCategoryDTO = relatedProductCategoryDTO;
        RelatedMeasureUnitDTO = relatedMeasureUnitDTO;
        RelatedBrandDTO = relatedBrandDTO;
        return this;
    }


    public ProductPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteProduct>? listRepeatedInputIdentityDelete, ProductDTO originalProductDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalProductDTO = originalProductDTO;
        return this;
    }
}