using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class MeasureUnit : BaseEntity<MeasureUnit, InputCreateMeasureUnit, InputUpdateMeasureUnit, OutputMeasureUnit, MeasureUnitDTO, InternalPropertiesMeasureUnitDTO, ExternalPropertiesMeasureUnitDTO, AuxiliaryPropertiesMeasureUnitDTO>, IBaseEntity<MeasureUnit, MeasureUnitDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }

    public virtual List<Product>? ListProduct { get; private set; }

    public MeasureUnit() { }

    public MeasureUnit(string code, string description, List<Product>? listProduct)
    {
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    public MeasureUnitDTO GetDTO(MeasureUnit entity)
    {
        return new MeasureUnitDTO
        {
            InternalPropertiesDTO = new InternalPropertiesMeasureUnitDTO().SetInternalData(entity.Id, entity.CreationDate, entity.ChangeDate, entity.CreationUserId, entity.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesMeasureUnitDTO(entity.Code, entity.Description),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesMeasureUnitDTO((from i in entity.ListProduct ?? new List<Product>() select (ProductDTO)(dynamic)i).ToList()).SetInternalData(entity.CreationUser!, entity.ChangeUser!)
        };
    }

    public MeasureUnit GetEntity(MeasureUnitDTO dto)
    {
        return new MeasureUnit(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, (from i in dto.AuxiliaryPropertiesDTO.ListProduct ?? new List<ProductDTO>() select (Product)(dynamic)i).ToList())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator MeasureUnitDTO?(MeasureUnit entity)
    {
        return entity == null ? default : new MeasureUnit().GetDTO(entity);
    }

    public static implicit operator MeasureUnit?(MeasureUnitDTO dto)
    {
        return dto == null ? default : new MeasureUnit().GetEntity(dto);
    }
}