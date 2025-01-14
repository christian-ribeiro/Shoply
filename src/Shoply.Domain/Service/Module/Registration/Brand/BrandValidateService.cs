using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class BrandValidateService(ITranslationService translationService) : BaseValidateService<BrandValidateDTO>(translationService), IBrandValidateService
{
    public void Create(List<BrandValidateDTO> listBrandValidateDTO)
    {
        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.ListRepeatedInputCreate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Code, 1, 6)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = resultInvalidLength == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Code, 1, 6, resultInvalidLength, nameof(i.InputCreate.Code))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Description, 1, 100, resultInvalidLength, nameof(i.InputCreate.Description))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.OriginalBrandDTO != null
             let setInvalid = i.SetInvalid()
             select AlreadyExists(i.InputCreate!.Code)).ToList();

        _ = (from i in RemoveInvalid(listBrandValidateDTO)
             select AddSuccessMessage(i.InputCreate!.Code, GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Marca"))).ToList();
    }

    public void Update(List<BrandValidateDTO> listBrandValidateDTO)
    {
        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.InputIdentityUpdate?.InputUpdate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.OriginalBrandDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalBrandDTO!.ExternalPropertiesDTO!.Code, i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Description))).ToList();

        _ = (from i in RemoveInvalid(listBrandValidateDTO)
             select AddSuccessMessage(i.OriginalBrandDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Marca"))).ToList();
    }

    public void Delete(List<BrandValidateDTO> listBrandValidateDTO)
    {
        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.OriginalBrandDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.InputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listBrandValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listBrandValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listBrandValidateDTO)
             select AddSuccessMessage(i.OriginalBrandDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Marca"))).ToList();
    }
}