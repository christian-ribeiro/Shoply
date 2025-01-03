using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Domain.Utils;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class Brand : BaseEntity<Brand, InputCreateBrand, InputUpdateBrand, OutputBrand, BrandDTO, InternalPropertiesBrandDTO, ExternalPropertiesBrandDTO, AuxiliaryPropertiesBrandDTO>, IBaseEntity<Brand, BrandDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }

    public virtual List<Product>? ListProduct { get; private set; }

    public Brand() { }

    public Brand(string code, string description, List<Product>? listProduct)
    {
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    public BrandDTO GetDTO(Brand entity)
    {
        return new BrandDTO
        {
            InternalPropertiesDTO = new InternalPropertiesBrandDTO().SetInternalData(entity.Id, entity.CreationDate, entity.ChangeDate, entity.CreationUserId, entity.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesBrandDTO(entity.Code, entity.Description),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesBrandDTO(entity.ListProduct.Cast<ProductDTO>()).SetInternalData(entity.CreationUser!, entity.ChangeUser!)
        };
    }

    public Brand GetEntity(BrandDTO dto)
    {
        return new Brand(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, dto.AuxiliaryPropertiesDTO.ListProduct.Cast<Product>())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator BrandDTO?(Brand entity)
    {
        return entity == null ? default : new Brand().GetDTO(entity);
    }

    public static implicit operator Brand?(BrandDTO dto)
    {
        return dto == null ? default : new Brand().GetEntity(dto);
    }
}