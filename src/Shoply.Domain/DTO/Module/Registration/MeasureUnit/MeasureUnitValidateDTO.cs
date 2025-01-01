using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class MeasureUnitValidateDTO : MeasureUnitPropertyValidateDTO
{
    public InputCreateMeasureUnit? InputCreate { get; private set; }
    public InputIdentityUpdateMeasureUnit? InputIdentityUpdate { get; private set; }
    public InputIdentityDeleteMeasureUnit? InputIdentityDelete { get; private set; }

    public MeasureUnitValidateDTO ValidateCreate(InputCreateMeasureUnit? inputCreate, List<InputCreateMeasureUnit>? listRepeatedInputCreate, MeasureUnitDTO originalMeasureUnitDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(listRepeatedInputCreate, originalMeasureUnitDTO);
        return this;
    }

    public MeasureUnitValidateDTO ValidateUpdate(InputIdentityUpdateMeasureUnit? inputIdentityUpdate, List<InputIdentityUpdateMeasureUnit>? listRepeatedInputIdentityUpdate, MeasureUnitDTO originalMeasureUnitDTO)
    {
        InputIdentityUpdate = inputIdentityUpdate;
        ValidateUpdate(listRepeatedInputIdentityUpdate, originalMeasureUnitDTO);
        return this;
    }

    public MeasureUnitValidateDTO ValidateDelete(InputIdentityDeleteMeasureUnit? inputIdentityDelete, List<InputIdentityDeleteMeasureUnit>? listRepeatedInputIdentityDelete, MeasureUnitDTO originalMeasureUnitDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalMeasureUnitDTO);
        return this;
    }
}