using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductImageValidateService(ITranslationService translationService) : BaseValidateService<ProductImageValidateDTO>(translationService), IProductImageValidateService
{
    public void Create(List<ProductImageValidateDTO> listProductImageValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductImageValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductImageValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductImageValidateDTO)
             where i.ListRepeatedInputCreate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductImageValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductImageValidateDTO)
             where string.IsNullOrEmpty(i.InputCreate!.FileName)
             let setInvalid = i.SetIgnore()
             select Invalid(listProductImageValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductImageValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedProductDTO, i.InputCreate!.ProductId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidRelatedProperty(i.InputCreate!.FileName, i.InputCreate.ProductId, nameof(i.InputCreate.ProductId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveInvalid(listProductImageValidateDTO)
             select AddSuccessMessage(i.InputCreate!.FileName, GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Imagem Produto"))).ToList();
    }

    public void Delete(List<ProductImageValidateDTO> listProductImageValidateDTO)
    {
        _ = (from i in RemoveIgnore(listProductImageValidateDTO)
             where i.OriginalProductImageDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductImageValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductImageValidateDTO)
             where i.InputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listProductImageValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listProductImageValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listProductImageValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listProductImageValidateDTO)
             select AddSuccessMessage(i.OriginalProductImageDTO!.ExternalPropertiesDTO.FileName, GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Imagem Produto"))).ToList();
    }
}