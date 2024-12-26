using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class AuxiliaryPropertiesProductDTO : BaseAuxiliaryPropertiesDTO<AuxiliaryPropertiesProductDTO>
{
    public ProductCategoryDTO? ProductCategory { get; private set; }
    public MeasureUnitDTO? MeasureUnit { get; private set; }
    public BrandDTO? Brand { get; private set; }
    public List<ProductImageDTO>? ListProductImage { get; private set; }

    public AuxiliaryPropertiesProductDTO() { }

    public AuxiliaryPropertiesProductDTO(ProductCategoryDTO? productCategory, MeasureUnitDTO? measureUnit, BrandDTO? brand, List<ProductImageDTO>? listProductImage)
    {
        ProductCategory = productCategory;
        MeasureUnit = measureUnit;
        Brand = brand;
        ListProductImage = listProductImage;
    }
}