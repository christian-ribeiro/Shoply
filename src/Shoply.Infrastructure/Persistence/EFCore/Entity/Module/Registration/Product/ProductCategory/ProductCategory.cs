using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class ProductCategory : BaseEntity<ProductCategory, InputCreateProductCategory, InputUpdateProductCategory, OutputProductCategory, ProductCategoryDTO, InternalPropertiesProductCategoryDTO, ExternalPropertiesProductCategoryDTO, AuxiliaryPropertiesProductCategoryDTO>, IBaseEntity<ProductCategory, ProductCategoryDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }

    public virtual List<Product>? ListProduct { get; private set; }

    public ProductCategory() { }

    public ProductCategory(string code, string description, List<Product>? listProduct)
    {
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    public ProductCategoryDTO GetDTO(ProductCategory entity)
    {
        return new ProductCategoryDTO
        {
            InternalPropertiesDTO = new InternalPropertiesProductCategoryDTO().SetInternalData(entity.Id, entity.CreationDate, entity.ChangeDate, entity.CreationUserId, entity.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesProductCategoryDTO(entity.Code, entity.Description),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductCategoryDTO((from i in entity.ListProduct ?? new List<Product>() select (ProductDTO)(dynamic)i).ToList()).SetInternalData(entity.CreationUser!, entity.ChangeUser!)
        };
    }

    public ProductCategory GetEntity(ProductCategoryDTO dto)
    {
        return new ProductCategory(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, (from i in dto.AuxiliaryPropertiesDTO.ListProduct ?? new List<ProductDTO>() select (Product)(dynamic)i).ToList())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator ProductCategoryDTO?(ProductCategory entity)
    {
        return entity == null ? default : new ProductCategory().GetDTO(entity);
    }

    public static implicit operator ProductCategory?(ProductCategoryDTO dto)
    {
        return dto == null ? default : new ProductCategory().GetEntity(dto);
    }
}