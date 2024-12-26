using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class MeasureUnitPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateMeasureUnit>? ListRepeatedInputCreateMeasureUnit { get; private set; }
    public List<InputIdentityUpdateMeasureUnit>? ListRepeatedInputIdentityUpdateMeasureUnit { get; private set; }
    public List<InputIdentityDeleteMeasureUnit>? ListRepeatedInputIdentityDeleteMeasureUnit { get; private set; }
    public MeasureUnitDTO? OriginalMeasureUnitDTO { get; private set; }

    public MeasureUnitPropertyValidateDTO ValidateCreate(List<InputCreateMeasureUnit>? listRepeatedInputCreateMeasureUnit, MeasureUnitDTO? originalMeasureUnitDTO)
    {
        ListRepeatedInputCreateMeasureUnit = listRepeatedInputCreateMeasureUnit;
        OriginalMeasureUnitDTO = originalMeasureUnitDTO;
        return this;
    }

    public MeasureUnitPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateMeasureUnit>? listRepeatedInputIdentityUpdateMeasureUnit, MeasureUnitDTO originalMeasureUnitDTO)
    {
        ListRepeatedInputIdentityUpdateMeasureUnit = listRepeatedInputIdentityUpdateMeasureUnit;
        OriginalMeasureUnitDTO = originalMeasureUnitDTO;
        return this;
    }


    public MeasureUnitPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteMeasureUnit>? listRepeatedInputIdentityDeleteMeasureUnit, MeasureUnitDTO originalMeasureUnitDTO)
    {
        ListRepeatedInputIdentityDeleteMeasureUnit = listRepeatedInputIdentityDeleteMeasureUnit;
        OriginalMeasureUnitDTO = originalMeasureUnitDTO;
        return this;
    }
}