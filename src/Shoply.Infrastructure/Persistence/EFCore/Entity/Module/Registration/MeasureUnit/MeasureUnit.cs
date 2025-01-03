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
        throw new NotImplementedException();
    }

    public MeasureUnit GetEntity(MeasureUnitDTO dto)
    {
        throw new NotImplementedException();
    }
}