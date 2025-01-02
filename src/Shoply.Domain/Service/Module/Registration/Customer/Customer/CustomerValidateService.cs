using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerValidateService(ITranslationService translationService) : BaseValidateService<CustomerValidateDTO>(translationService), ICustomerValidateService
{
    public void Create(List<CustomerValidateDTO> listCustomerValidateDTO)
    {
        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.ListRepeatedInputCreate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Code, 1, 6)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = resultInvalidLength == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.Code, 1, 6, resultInvalidLength, nameof(i.InputCreate.Code))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.OriginalCustomerDTO != null
             let setInvalid = i.SetInvalid()
             select AlreadyExists(i.InputCreate!.Code)).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.FirstName, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.FirstName, 1, 100, resultInvalidLength, nameof(i.InputCreate.FirstName))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.LastName, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Code, i.InputCreate.LastName, 1, 100, resultInvalidLength, nameof(i.InputCreate.LastName))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             let resultInvalid = ValidatePersonType(i)
             select true).ToList();

        _ = (from i in RemoveInvalid(listCustomerValidateDTO)
             select AddSuccessMessage(i.InputCreate!.Code, GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Cliente"))).ToList();
    }

    private bool ValidatePersonType(CustomerValidateDTO customerValidateDTO)
    {
        if (!Enum.IsDefined(customerValidateDTO.InputCreate!.PersonType))
        {
            customerValidateDTO.SetInvalid();
            ManualNotification(customerValidateDTO.InputCreate!.Code, "Tipo de pessoa inválido", EnumValidateType.Invalid);
        }

        switch (customerValidateDTO.InputCreate!.PersonType)
        {
            case EnumPersonType.Natural:
                var resultInvalidCFP = InvalidCPF(customerValidateDTO.InputCreate.Document);
                if (resultInvalidCFP != EnumValidateType.Valid)
                {
                    customerValidateDTO.SetInvalid();
                    InvalidGeneric(customerValidateDTO.InputCreate.Code, customerValidateDTO.InputCreate.Document, "CPF", resultInvalidCFP);
                }

                var resultInvalidBirthDate = InvalidBirthDate(customerValidateDTO.InputCreate.BirthDate, 18, false);
                if (resultInvalidBirthDate != EnumValidateType.Valid)
                {
                    customerValidateDTO.SetInvalid();
                    InvalidBirthDate(customerValidateDTO.InputCreate.Code, 18, resultInvalidBirthDate, nameof(customerValidateDTO.InputCreate.BirthDate));
                }
                break;
            case EnumPersonType.Legal:
                var resultInvalidCNPJ = InvalidCNPJ(customerValidateDTO.InputCreate.Document);
                if (resultInvalidCNPJ != EnumValidateType.Valid)
                {
                    customerValidateDTO.SetInvalid();
                    InvalidGeneric(customerValidateDTO.InputCreate.Code, customerValidateDTO.InputCreate.Document, "CNPJ", resultInvalidCNPJ);
                }

                if (customerValidateDTO.InputCreate.BirthDate != null)
                {
                    customerValidateDTO.SetInvalid();
                    ManualNotification(customerValidateDTO.InputCreate.Code, "Pessoa jurídica não possui data de nascimento", EnumValidateType.Invalid);
                }
                break;
            default:
                customerValidateDTO.SetInvalid();
                InvalidGeneric(customerValidateDTO.InputCreate.Code, customerValidateDTO.InputCreate.PersonType, nameof(customerValidateDTO.InputCreate.PersonType), EnumValidateType.Invalid);
                break;
        }

        return true;
    }

    public void Update(List<CustomerValidateDTO> listCustomerValidateDTO)
    {
        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.InputIdentityUpdate?.InputUpdate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.OriginalCustomerDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.LastName, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalCustomerDTO!.ExternalPropertiesDTO!.Code, i.InputIdentityUpdate!.InputUpdate!.LastName, 1, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.LastName))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.LastName, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalCustomerDTO!.ExternalPropertiesDTO!.Code, i.InputIdentityUpdate!.InputUpdate!.LastName, 1, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.LastName))).ToList();

        _ = (from i in RemoveInvalid(listCustomerValidateDTO)
             select AddSuccessMessage(i.OriginalCustomerDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Cliente"))).ToList();
    }

    public void Delete(List<CustomerValidateDTO> listCustomerValidateDTO)
    {
        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.ListRepeatedInputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.OriginalCustomerDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listCustomerValidateDTO)
             select AddSuccessMessage(i.OriginalCustomerDTO!.ExternalPropertiesDTO.Code, GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Cliente"))).ToList();
    }
}