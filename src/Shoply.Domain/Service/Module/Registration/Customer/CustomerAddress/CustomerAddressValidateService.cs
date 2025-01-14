using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerAddressValidateService(ITranslationService translationService) : BaseValidateService<CustomerAddressValidateDTO>(translationService), ICustomerAddressValidateService
{
    public void Create(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO)
    {
        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidRelatedProperty = InvalidRelatedProperty(i.RelatedCustomerDTO, i.InputCreate!.CustomerId)
             where resultInvalidRelatedProperty != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidRelatedProperty($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.CustomerId, nameof(i.InputCreate.CustomerId), resultInvalidRelatedProperty)).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.PublicPlace, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidLength($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.PublicPlace, 1, 100, resultInvalidLength, nameof(i.InputCreate.PublicPlace))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Number, 1, 10)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidLength($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.Number, 1, 10, resultInvalidLength, nameof(i.InputCreate.Number))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Complement, 0, 50)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidLength($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.Complement, 0, 50, resultInvalidLength, nameof(i.InputCreate.Complement))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Neighborhood, 1, 50)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidLength($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.Neighborhood, 1, 50, resultInvalidLength, nameof(i.InputCreate.Neighborhood))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.PostalCode, 1, 8)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidLength($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.PostalCode, 1, 8, resultInvalidLength, nameof(i.InputCreate.PostalCode))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Reference, 0, 200)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidLength($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.Reference, 0, 200, resultInvalidLength, nameof(i.InputCreate.Reference))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Observation, 0, 400)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetIgnore()
             select InvalidLength($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", i.InputCreate!.Observation, 0, 400, resultInvalidLength, nameof(i.InputCreate.Observation))).ToList();

        _ = (from i in RemoveInvalid(listCustomerAddressValidateDTO)
             select AddSuccessMessage($"{i.InputCreate!.CustomerId}-{i.InputCreate!.AddressType}-{i.InputCreate!.PublicPlace}", GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Endereço Cliente"))).ToList();
    }

    public void Update(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO)
    {
        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.InputIdentityUpdate?.InputUpdate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.OriginalCustomerAddressDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.PublicPlace, 1, 100)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", i.InputIdentityUpdate!.InputUpdate!.PublicPlace, 1, 100, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.PublicPlace))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Number, 1, 10)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", i.InputIdentityUpdate!.InputUpdate!.Number, 1, 10, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Number))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Complement, 1, 50)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", i.InputIdentityUpdate!.InputUpdate!.Complement, 1, 50, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Complement))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Neighborhood, 1, 10)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", i.InputIdentityUpdate!.InputUpdate!.Neighborhood, 1, 10, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Neighborhood))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.PostalCode, 1, 8)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", i.InputIdentityUpdate!.InputUpdate!.PostalCode, 1, 8, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.PostalCode))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Reference, 0, 200)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", i.InputIdentityUpdate!.InputUpdate!.Reference, 0, 200, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Reference))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Observation, 0, 400)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", i.InputIdentityUpdate!.InputUpdate!.Observation, 0, 400, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Observation))).ToList();

        _ = (from i in RemoveInvalid(listCustomerAddressValidateDTO)
             select AddSuccessMessage($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Endereço Cliente"))).ToList();
    }

    public void Delete(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO)
    {
        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.OriginalCustomerAddressDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.InputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listCustomerAddressValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listCustomerAddressValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listCustomerAddressValidateDTO)
             select AddSuccessMessage($"{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.CustomerId}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.AddressType}-{i.OriginalCustomerAddressDTO!.ExternalPropertiesDTO!.PublicPlace}", GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Endereço Cliente"))).ToList();
    }
}