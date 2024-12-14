using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base;
using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerAddressService(ICustomerAddressRepository repository, ITranslationService translationService, ICustomerRepository customerRepository) : BaseService<ICustomerAddressRepository, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress, InputIdentityUpdateCustomerAddress, InputIdentityDeleteCustomerAddress, CustomerAddressValidateDTO, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO, EnumValidateProcessGeneric>(repository, translationService), ICustomerAddressService
{
    internal override async Task ValidateProcess(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO, EnumValidateProcessGeneric processType)
    {
        switch (processType)
        {
            case EnumValidateProcessGeneric.Create:
                foreach (var customerAddressValidateDTO in listCustomerAddressValidateDTO)
                {
                    if (customerAddressValidateDTO.InputCreateCustomerAddress == null)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    var resultInvalidRelatedProperty = await InvalidRelatedProperty(customerAddressValidateDTO.InputCreateCustomerAddress.CustomerId, customerAddressValidateDTO.RelatedCustomerDTO);
                    if (resultInvalidRelatedProperty != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidRelatedProperty(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.CustomerId, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.CustomerId), resultInvalidRelatedProperty);
                    }

                    var resultFirstNameInvalidLength = InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, 1, 100);
                    if (resultFirstNameInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, 1, 100, resultFirstNameInvalidLength, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace));
                    }

                    var resultNumberInvalidLength = InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.Number, 1, 10);
                    if (resultNumberInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.Number, 1, 10, resultNumberInvalidLength, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.Number));
                    }

                    var resultComplementInvalidLength = InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.Complement, 1, 50);
                    if (resultComplementInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.Complement, 1, 50, resultComplementInvalidLength, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.Complement));
                    }

                    var resultNeighborhoodInvalidLength = InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.Neighborhood, 1, 50);
                    if (resultNeighborhoodInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.Neighborhood, 1, 50, resultNeighborhoodInvalidLength, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.Neighborhood));
                    }

                    var resultPostalCodeInvalidLength = InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PostalCode, 1, 8);
                    if (resultPostalCodeInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.PostalCode, 1, 8, resultPostalCodeInvalidLength, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.PostalCode));
                    }

                    var resultReferenceInvalidLength = InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.Reference, 0, 200);
                    if (resultReferenceInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.Reference, 0, 200, resultReferenceInvalidLength, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.Reference));
                    }

                    var resultObservationInvalidLength = InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.Observation, 0, 400);
                    if (resultObservationInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, customerAddressValidateDTO.InputCreateCustomerAddress.Observation, 0, 400, resultObservationInvalidLength, nameof(customerAddressValidateDTO.InputCreateCustomerAddress.Observation));
                    }

                    if (!customerAddressValidateDTO.Invalid)
                        await AddSuccessMessage(customerAddressValidateDTO.InputCreateCustomerAddress.PublicPlace, await GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Endereço do Cliente"));
                }
                break;
            case EnumValidateProcessGeneric.Update:
                foreach (var customerAddressValidateDTO in listCustomerAddressValidateDTO)
                {
                    if (customerAddressValidateDTO.InputIdentityUpdateCustomerAddress == null)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    if (customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate == null)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    if (customerAddressValidateDTO.OriginalCustomerAddressDTO == null)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    var repeatedInputUpdate = customerAddressValidateDTO.ListRepeatedInputIdentityUpdateCustomerAddress?.Count > 0;
                    if (repeatedInputUpdate)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    var resultFirstNameInvalidLength = InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, 1, 100);
                    if (resultFirstNameInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, 1, 100, resultFirstNameInvalidLength, nameof(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace));
                    }

                    var resultNumberInvalidLength = InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Number, 1, 10);
                    if (resultNumberInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Number, 1, 10, resultNumberInvalidLength, nameof(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Number));
                    }

                    var resultComplementInvalidLength = InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Complement, 1, 50);
                    if (resultComplementInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Complement, 1, 50, resultComplementInvalidLength, nameof(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Complement));
                    }

                    var resultNeighborhoodInvalidLength = InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Neighborhood, 1, 50);
                    if (resultNeighborhoodInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Neighborhood, 1, 50, resultNeighborhoodInvalidLength, nameof(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Neighborhood));
                    }

                    var resultPostalCodeInvalidLength = InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PostalCode, 1, 8);
                    if (resultPostalCodeInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PostalCode, 1, 8, resultPostalCodeInvalidLength, nameof(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PostalCode));
                    }

                    var resultReferenceInvalidLength = InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Reference, 0, 200);
                    if (resultReferenceInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Reference, 0, 200, resultReferenceInvalidLength, nameof(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Reference));
                    }

                    var resultObservationInvalidLength = InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Observation, 0, 400);
                    if (resultObservationInvalidLength != EnumValidateType.Valid)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await InvalidLength(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.PublicPlace, customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Observation, 0, 400, resultObservationInvalidLength, nameof(customerAddressValidateDTO.InputIdentityUpdateCustomerAddress.InputUpdate.Observation));
                    }

                    if (!customerAddressValidateDTO.Invalid)
                        await AddSuccessMessage(customerAddressValidateDTO.OriginalCustomerAddressDTO.ExternalPropertiesDTO.PublicPlace, await GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Endereço do Cliente"));
                }
                break;
            case EnumValidateProcessGeneric.Delete:
                foreach (var customerAddressValidateDTO in listCustomerAddressValidateDTO)
                {
                    if (customerAddressValidateDTO.InputIdentityDeleteCustomerAddress == null)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    if (customerAddressValidateDTO.OriginalCustomerAddressDTO == null)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    var repeatedInputDelete = customerAddressValidateDTO.ListRepeatedInputIdentityDeleteCustomerAddress?.Count > 0;
                    if (repeatedInputDelete)
                    {
                        customerAddressValidateDTO.SetInvalid();
                        await Invalid(listCustomerAddressValidateDTO.IndexOf(customerAddressValidateDTO));
                        continue;
                    }

                    if (!customerAddressValidateDTO.Invalid)
                        await AddSuccessMessage(customerAddressValidateDTO.OriginalCustomerAddressDTO.ExternalPropertiesDTO.PublicPlace, await GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Endereço do Cliente"));
                }
                break;
        }
    }

    #region Create
    public override async Task<BaseResult<List<OutputCustomerAddress?>>> Create(List<InputCreateCustomerAddress> listInputCreateCustomerAddress)
    {
        List<CustomerDTO> listRelatedCustomerDTO = await customerRepository.GetListByListId((from i in listInputCreateCustomerAddress select i.CustomerId).ToList());

        var listCreate = (from i in listInputCreateCustomerAddress
                          select new
                          {
                              InputCreateCustomerAddress = i,
                              RelatedCustomerDTO = (from j in listRelatedCustomerDTO where j.InternalPropertiesDTO.Id == i.CustomerId select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO = (from i in listCreate select new CustomerAddressValidateDTO().ValidateCreate(i.InputCreateCustomerAddress, i.RelatedCustomerDTO)).ToList();
        await ValidateProcess(listCustomerAddressValidateDTO, EnumValidateProcessGeneric.Create);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateCustomerAddress.Count)
            return BaseResult<List<OutputCustomerAddress?>>.Failure(errors);

        List<CustomerAddressDTO> listCreateCustomerAddressDTO = (from i in RemoveInvalid(listCustomerAddressValidateDTO) select new CustomerAddressDTO().Create(i.InputCreateCustomerAddress!)).ToList();
        return BaseResult<List<OutputCustomerAddress?>>.Success(FromDTOToOutput(await _repository.Create(listCreateCustomerAddressDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputCustomerAddress?>>> Update(List<InputIdentityUpdateCustomerAddress> listInputIdentityUpdateCustomerAddress)
    {
        List<CustomerAddressDTO> listOriginalCustomerAddressDTO = await _repository.GetListByListId((from i in listInputIdentityUpdateCustomerAddress select i.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateCustomerAddress
                          select new
                          {
                              InputIdentityUpdateCustomerAddress = i,
                              ListRepeatedInputIdentityUpdateCustomerAddress = (from j in listInputIdentityUpdateCustomerAddress where listInputIdentityUpdateCustomerAddress.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalCustomerAddressDTO = (from j in listOriginalCustomerAddressDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO = (from i in listUpdate select new CustomerAddressValidateDTO().ValidateUpdate(i.InputIdentityUpdateCustomerAddress, i.ListRepeatedInputIdentityUpdateCustomerAddress, i.OriginalCustomerAddressDTO)).ToList();
        await ValidateProcess(listCustomerAddressValidateDTO, EnumValidateProcessGeneric.Update);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateCustomerAddress.Count)
            return BaseResult<List<OutputCustomerAddress?>>.Failure(errors);

        List<CustomerAddressDTO> listUpdateCustomerAddressDTO = (from i in RemoveInvalid(listCustomerAddressValidateDTO) select i.OriginalCustomerAddressDTO!.Update(i.InputIdentityUpdateCustomerAddress!.InputUpdate!)).ToList();
        return BaseResult<List<OutputCustomerAddress?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateCustomerAddressDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteCustomerAddress> listInputIdentityDeleteCustomerAddress)
    {
        List<CustomerAddressDTO> listOriginalCustomerAddressDTO = await _repository.GetListByListId((from i in listInputIdentityDeleteCustomerAddress select i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteCustomerAddress
                          select new
                          {
                              InputIdentityDeleteCustomerAddress = i,
                              ListRepeatedInputIdentityDeleteCustomerAddress = (from j in listInputIdentityDeleteCustomerAddress where listInputIdentityDeleteCustomerAddress.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalCustomerAddressDTO = (from j in listOriginalCustomerAddressDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO = (from i in listDelete select new CustomerAddressValidateDTO().ValidateDelete(i.InputIdentityDeleteCustomerAddress, i.ListRepeatedInputIdentityDeleteCustomerAddress, i.OriginalCustomerAddressDTO)).ToList();
        await ValidateProcess(listCustomerAddressValidateDTO, EnumValidateProcessGeneric.Delete);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteCustomerAddress.Count)
            return BaseResult<bool>.Failure(errors);

        List<CustomerAddressDTO> listDeletepdateCustomerAddressDTO = (from i in RemoveInvalid(listCustomerAddressValidateDTO) select i.OriginalCustomerAddressDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateCustomerAddressDTO), [.. successes, .. errors]);
    }
    #endregion
}