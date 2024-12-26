using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class Product : BaseEntity<Product, InputCreateProduct, InputUpdateProduct, OutputProduct, ProductDTO, InternalPropertiesProductDTO, ExternalPropertiesProductDTO, AuxiliaryPropertiesProductDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }
    public string? BarCode { get; private set; }
    public decimal CostValue { get; private set; }
    public decimal SaleValue { get; private set; }
    public EnumProductStatus Status { get; private set; }
    public long? ProductCategoryId { get; private set; }
    public long MeasureUnitId { get; private set; }
    public long BrandId { get; private set; }
    public decimal Markup { get; private set; }

    public virtual ProductCategory? ProductCategory { get; private set; }
    public virtual MeasureUnit? MeasureUnit { get; private set; }
    public virtual Brand? Brand { get; private set; }

    public virtual List<ProductImage>? ListProductImage { get; private set; }
    public Product() { }

    public Product(string code, string description, string? barCode, decimal costValue, decimal saleValue, EnumProductStatus status, long? productCategoryId, long measureUnitId, long brandId, decimal markup, ProductCategory? productCategory, MeasureUnit? measureUnit, Brand? brand, List<ProductImage>? listProductImage)
    {
        Code = code;
        Description = description;
        BarCode = barCode;
        CostValue = costValue;
        SaleValue = saleValue;
        Status = status;
        ProductCategoryId = productCategoryId;
        MeasureUnitId = measureUnitId;
        BrandId = brandId;
        Markup = markup;
        ProductCategory = productCategory;
        MeasureUnit = measureUnit;
        Brand = brand;
        ListProductImage = listProductImage;
    }
}