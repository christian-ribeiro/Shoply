using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class MeasureUnitPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateMeasureUnit>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateMeasureUnit>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteMeasureUnit>? ListRepeatedInputIdentityDelete { get; private set; }
    public MeasureUnitDTO? OriginalMeasureUnitDTO { get; private set; }

    public MeasureUnitPropertyValidateDTO ValidateCreate(List<InputCreateMeasureUnit>? listRepeatedInputCreate, MeasureUnitDTO? originalMeasureUnitDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreate;
        OriginalMeasureUnitDTO = originalMeasureUnitDTO;
        return this;
    }

    public MeasureUnitPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateMeasureUnit>? listRepeatedInputIdentityUpdate, MeasureUnitDTO originalMeasureUnitDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdate;
        OriginalMeasureUnitDTO = originalMeasureUnitDTO;
        return this;
    }


    public MeasureUnitPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteMeasureUnit>? listRepeatedInputIdentityDelete, MeasureUnitDTO originalMeasureUnitDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalMeasureUnitDTO = originalMeasureUnitDTO;
        return this;
    }
}