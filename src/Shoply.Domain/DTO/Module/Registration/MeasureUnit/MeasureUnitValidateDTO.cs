using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class MeasureUnitValidateDTO : MeasureUnitPropertyValidateDTO
{
    public InputCreateMeasureUnit? InputCreateMeasureUnit { get; private set; }
    public InputIdentityUpdateMeasureUnit? InputIdentityUpdateMeasureUnit { get; private set; }
    public InputIdentityDeleteMeasureUnit? InputIdentityDeleteMeasureUnit { get; private set; }

    public MeasureUnitValidateDTO ValidateCreate(InputCreateMeasureUnit? inputCreateMeasureUnit, List<InputCreateMeasureUnit>? listRepeatedInputCreateMeasureUnit, MeasureUnitDTO originalMeasureUnitDTO)
    {
        InputCreateMeasureUnit = inputCreateMeasureUnit;
        ValidateCreate(listRepeatedInputCreateMeasureUnit, originalMeasureUnitDTO);
        return this;
    }

    public MeasureUnitValidateDTO ValidateUpdate(InputIdentityUpdateMeasureUnit? inputIdentityUpdateMeasureUnit, List<InputIdentityUpdateMeasureUnit>? listRepeatedInputIdentityUpdateMeasureUnit, MeasureUnitDTO originalMeasureUnitDTO)
    {
        InputIdentityUpdateMeasureUnit = inputIdentityUpdateMeasureUnit;
        ValidateUpdate(listRepeatedInputIdentityUpdateMeasureUnit, originalMeasureUnitDTO);
        return this;
    }

    public MeasureUnitValidateDTO ValidateDelete(InputIdentityDeleteMeasureUnit? inputIdentityDeleteMeasureUnit, List<InputIdentityDeleteMeasureUnit>? listRepeatedInputIdentityDeleteMeasureUnit, MeasureUnitDTO originalMeasureUnitDTO)
    {
        InputIdentityDeleteMeasureUnit = inputIdentityDeleteMeasureUnit;
        ValidateDelete(listRepeatedInputIdentityDeleteMeasureUnit, originalMeasureUnitDTO);
        return this;
    }
}