using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;
using Shoply.Domain.Utils;

namespace Shoply.Domain.DTO.Module.Registration;

public class MeasureUnitDTO : BaseDTO<InputCreateMeasureUnit, InputUpdateMeasureUnit, OutputMeasureUnit, MeasureUnitDTO, InternalPropertiesMeasureUnitDTO, ExternalPropertiesMeasureUnitDTO, AuxiliaryPropertiesMeasureUnitDTO>, IBaseDTO<MeasureUnitDTO, OutputMeasureUnit>
{
    public MeasureUnitDTO GetDTO(OutputMeasureUnit output)
    {
        return new MeasureUnitDTO
        {
            InternalPropertiesDTO = new InternalPropertiesMeasureUnitDTO().SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesMeasureUnitDTO(output.Code, output.Description),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesMeasureUnitDTO(output.ListProduct.Cast<ProductDTO>()).SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }
    public OutputMeasureUnit GetOutput(MeasureUnitDTO dto)
    {
        return new OutputMeasureUnit(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, dto.AuxiliaryPropertiesDTO.ListProduct.Cast<OutputProduct>())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator MeasureUnitDTO?(OutputMeasureUnit output)
    {
        return output == null ? default : new MeasureUnitDTO().GetDTO(output);
    }

    public static implicit operator OutputMeasureUnit?(MeasureUnitDTO dto)
    {
        return dto == null ? default : dto.GetOutput(dto);
    }
}