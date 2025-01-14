using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductValidateService(ITranslationService translationService) : BaseValidateService<ProductValidateDTO>(translationService), IProductValidateService
{
    public void Create(List<ProductValidateDTO> listProductValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.ListRepeatedInputCreate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Code, 1, 6)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = resultInvalidLength == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Code, 1, 6, resultInvalidLength, nameof(i.InputCreate.Code))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.BarCode, 1, 100, resultInvalidLength, nameof(i.InputCreate.Description))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.OriginalProductDTO != null
             let setInvalid = i.SetInvalid()
             select AlreadyExists(i.InputCreate!.Code)).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.BarCode, 0, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.BarCode, 0, 100, resultInvalidLength, nameof(i.InputCreate.BarCode))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedProductCategoryDTO, i.InputCreate!.ProductCategoryId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidRelatedProperty(i.InputCreate!.Code, i.InputCreate.ProductCategoryId, nameof(i.InputCreate.ProductCategoryId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedMeasureUnitDTO, i.InputCreate!.MeasureUnitId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidRelatedProperty(i.InputCreate!.Code, i.InputCreate.MeasureUnitId, nameof(i.InputCreate.MeasureUnitId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedBrandDTO, i.InputCreate!.BrandId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidRelatedProperty(i.InputCreate!.Code, i.InputCreate.BrandId, nameof(i.InputCreate.BrandId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveInvalid(listProductValidateDTO)
             select AddSuccessMessage(i.InputCreate!.Code, GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Produto"))).ToList();
    }

    public void Update(List<ProductValidateDTO> listProductValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.InputIdentityUpdate?.InputUpdate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.OriginalProductDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalProductDTO!.ExternalPropertiesDTO!.Code, i.InputIdentityUpdate!.InputUpdate!.Description, 1, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Description))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.BarCode, 0, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalProductDTO!.ExternalPropertiesDTO!.Code, i.InputIdentityUpdate!.InputUpdate!.BarCode, 0, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.BarCode))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedProductCategoryDTO, i.InputIdentityUpdate!.InputUpdate!.ProductCategoryId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidRelatedProperty(i.OriginalProductDTO!.ExternalPropertiesDTO.Code, i.InputIdentityUpdate!.InputUpdate!.ProductCategoryId, nameof(i.InputIdentityUpdate.InputUpdate.ProductCategoryId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedMeasureUnitDTO, i.InputIdentityUpdate!.InputUpdate!.MeasureUnitId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidRelatedProperty(i.OriginalProductDTO!.ExternalPropertiesDTO.Code, i.InputIdentityUpdate!.InputUpdate!.MeasureUnitId, nameof(i.InputIdentityUpdate.InputUpdate.MeasureUnitId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedBrandDTO, i.InputIdentityUpdate!.InputUpdate!.BrandId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidRelatedProperty(i.OriginalProductDTO!.ExternalPropertiesDTO.Code, i.InputIdentityUpdate!.InputUpdate!.BrandId, nameof(i.InputIdentityUpdate.InputUpdate.BrandId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveInvalid(listProductValidateDTO)
             select AddSuccessMessage(i.OriginalProductDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Produto"))).ToList();
    }

    public void Delete(List<ProductValidateDTO> listProductValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.OriginalProductDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.InputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listProductValidateDTO)
             select AddSuccessMessage(i.OriginalProductDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Produto"))).ToList();
    }
}