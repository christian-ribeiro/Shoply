using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class MeasureUnitValidateService(ITranslationService translationService) : BaseValidateService<MeasureUnitValidateDTO>(translationService), IMeasureUnitValidateService
{
    public void Create(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO)
    {
        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.ListRepeatedInputCreate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Code, 1, 6)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = resultInvalidLength == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Code, 1, 6, resultInvalidLength, nameof(i.InputCreate.Code))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Code, 1, 100, resultInvalidLength, nameof(i.InputCreate.Code))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.OriginalMeasureUnitDTO != null
             let setInvalid = i.SetInvalid()
             select AlreadyExists(i.InputCreate!.Code)).ToList();

        _ = (from i in RemoveInvalid(listMeasureUnitValidateDTO)
             select AddSuccessMessage(i.InputCreate!.Code, GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Unidade de Medida"))).ToList();
    }

    public void Update(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO)
    {
        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.InputIdentityUpdate?.InputUpdate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.OriginalMeasureUnitDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalMeasureUnitDTO!.ExternalPropertiesDTO!.Code, i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Description))).ToList();

        _ = (from i in RemoveInvalid(listMeasureUnitValidateDTO)
             select AddSuccessMessage(i.OriginalMeasureUnitDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Unidade de Medida"))).ToList();
    }

    public void Delete(List<MeasureUnitValidateDTO> listMeasureUnitValidateDTO)
    {
        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.OriginalMeasureUnitDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.InputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listMeasureUnitValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listMeasureUnitValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listMeasureUnitValidateDTO)
             select AddSuccessMessage(i.OriginalMeasureUnitDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Unidade de Medida"))).ToList();
    }
}