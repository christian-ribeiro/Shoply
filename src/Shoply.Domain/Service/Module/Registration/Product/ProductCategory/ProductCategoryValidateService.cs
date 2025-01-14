using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductCategoryValidateService(ITranslationService translationService) : BaseValidateService<ProductCategoryValidateDTO>(translationService), IProductCategoryValidateService
{
    public void Create(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.ListRepeatedInputCreate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Code, 1, 6)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = resultInvalidLength == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Code, 1, 6, resultInvalidLength, nameof(i.InputCreate.Code))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Code, 1, 100, resultInvalidLength, nameof(i.InputCreate.Code))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.OriginalProductCategoryDTO != null
             let setInvalid = i.SetInvalid()
             select AlreadyExists(i.InputCreate!.Code)).ToList();

        _ = (from i in RemoveInvalid(listProductCategoryValidateDTO)
             select AddSuccessMessage(i.InputCreate!.Code, GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Categoria Produto"))).ToList();
    }

    public void Update(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.InputIdentityUpdate?.InputUpdate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.OriginalProductCategoryDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalProductCategoryDTO!.ExternalPropertiesDTO!.Code, i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Description))).ToList();

        _ = (from i in RemoveInvalid(listProductCategoryValidateDTO)
             select AddSuccessMessage(i.OriginalProductCategoryDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Categoria Produto"))).ToList();
    }

    public void Delete(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.OriginalProductCategoryDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.InputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductCategoryValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductCategoryValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listProductCategoryValidateDTO)
             select AddSuccessMessage(i.OriginalProductCategoryDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Categoria Produto"))).ToList();
    }
}