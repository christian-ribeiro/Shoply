using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class MeasureUnitDTO : BaseDTO<InputCreateMeasureUnit, InputUpdateMeasureUnit, OutputMeasureUnit, MeasureUnitDTO, InternalPropertiesMeasureUnitDTO, ExternalPropertiesMeasureUnitDTO, AuxiliaryPropertiesMeasureUnitDTO>, IBaseDTO<MeasureUnitDTO, OutputMeasureUnit>
{
    public MeasureUnitDTO? GetDTO(OutputMeasureUnit output)
    {
        throw new NotImplementedException();
    }

    public OutputMeasureUnit? GetOutput(MeasureUnitDTO dto)
    {
        throw new NotImplementedException();
    }
}