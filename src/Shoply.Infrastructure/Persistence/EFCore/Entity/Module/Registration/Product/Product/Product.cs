using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class Product : BaseEntity<Product, InputCreateProduct, InputUpdateProduct, OutputProduct, ProductDTO, InternalPropertiesProductDTO, ExternalPropertiesProductDTO, AuxiliaryPropertiesProductDTO>, IBaseEntity<Product, ProductDTO>
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

    public ProductDTO GetDTO(Product entity)
    {
        return new ProductDTO
        {
            InternalPropertiesDTO = new InternalPropertiesProductDTO(entity.Markup).SetInternalData(entity.Id, entity.CreationDate, entity.ChangeDate, entity.CreationUserId, entity.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesProductDTO(entity.Code, entity.Description, entity.BarCode, entity.CostValue, entity.SaleValue, entity.Status, entity.ProductCategoryId, entity.MeasureUnitId, entity.BrandId),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductDTO(entity.ProductCategory!, entity.MeasureUnit!, entity.Brand!, (from i in entity.ListProductImage ?? new List<ProductImage>() select (ProductImageDTO)(dynamic)i).ToList()).SetInternalData(entity.CreationUser!, entity.ChangeUser!)
        };
    }

    public Product GetEntity(ProductDTO dto)
    {
        return new Product(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, dto.ExternalPropertiesDTO.BarCode, dto.ExternalPropertiesDTO.CostValue, dto.ExternalPropertiesDTO.SaleValue, dto.ExternalPropertiesDTO.Status, dto.ExternalPropertiesDTO.ProductCategoryId, dto.ExternalPropertiesDTO.MeasureUnitId, dto.ExternalPropertiesDTO.BrandId, dto.InternalPropertiesDTO.Markup, dto.AuxiliaryPropertiesDTO.ProductCategory!, dto.AuxiliaryPropertiesDTO.MeasureUnit!, dto.AuxiliaryPropertiesDTO.Brand!, (from i in dto.AuxiliaryPropertiesDTO.ListProductImage ?? new List<ProductImageDTO>() select (ProductImage)(dynamic)i).ToList())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator ProductDTO?(Product entity)
    {
        return entity == null ? default : new Product().GetDTO(entity);
    }

    public static implicit operator Product?(ProductDTO dto)
    {
        return dto == null ? default : new Product().GetEntity(dto);
    }
}