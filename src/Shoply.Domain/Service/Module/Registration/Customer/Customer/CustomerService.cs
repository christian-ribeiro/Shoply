using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base;
using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerService(ICustomerRepository repository, ITranslationService translationService) : BaseService<ICustomerRepository, InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, InputIdentifierCustomer, OutputCustomer, CustomerValidateDTO, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO, EnumValidateProcessGeneric>(repository, translationService), ICustomerService
{
    internal override async Task ValidateProcess(List<CustomerValidateDTO> listCustomerValidateDTO, EnumValidateProcessGeneric processType)
    {
        switch (processType)
        {
            case EnumValidateProcessGeneric.Create:
                foreach (var customerValidateDTO in listCustomerValidateDTO)
                {
                    if (customerValidateDTO.InputCreateCustomer == null)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    var repeatedCustomer = customerValidateDTO.ListRepeatedInputCreateCustomer?.Count > 0;
                    if (repeatedCustomer)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    if (customerValidateDTO.OriginalCustomerDTO != null)
                    {
                        customerValidateDTO.SetInvalid();
                        await AlreadyExists(customerValidateDTO.InputCreateCustomer.Code);
                    }

                    var resultFirstNameInvalidLength = InvalidLength(customerValidateDTO.InputCreateCustomer.FirstName, 1, 100);
                    if (resultFirstNameInvalidLength != EnumValidateType.Valid)
                    {
                        customerValidateDTO.SetInvalid();
                        await InvalidLength(customerValidateDTO.InputCreateCustomer.Code, customerValidateDTO.InputCreateCustomer.FirstName, 1, 100, resultFirstNameInvalidLength, nameof(customerValidateDTO.InputCreateCustomer.FirstName));
                    }

                    var resultLastNameInvalidLength = InvalidLength(customerValidateDTO.InputCreateCustomer.LastName, 1, 100);
                    if (resultLastNameInvalidLength != EnumValidateType.Valid)
                    {
                        customerValidateDTO.SetInvalid();
                        await InvalidLength(customerValidateDTO.InputCreateCustomer.Code, customerValidateDTO.InputCreateCustomer.LastName, 1, 100, resultLastNameInvalidLength, nameof(customerValidateDTO.InputCreateCustomer.LastName));
                    }

                    switch (customerValidateDTO.InputCreateCustomer.PersonType)
                    {
                        case EnumPersonType.Natural:
                            var resultInvalidCFP = InvalidCPF(customerValidateDTO.InputCreateCustomer.Document);
                            if (resultInvalidCFP != EnumValidateType.Valid)
                            {
                                customerValidateDTO.SetInvalid();
                                await InvalidGeneric(customerValidateDTO.InputCreateCustomer.Code, customerValidateDTO.InputCreateCustomer.Document, "CPF", resultInvalidCFP);
                            }

                            var resultInvalidBirthDate = InvalidBirthDate(customerValidateDTO.InputCreateCustomer.BirthDate, 18, false);
                            if (resultInvalidBirthDate != EnumValidateType.Valid)
                            {
                                customerValidateDTO.SetInvalid();
                                await InvalidBirthDate(customerValidateDTO.InputCreateCustomer.Code, 18, resultInvalidBirthDate, nameof(customerValidateDTO.InputCreateCustomer.BirthDate));
                            }
                            break;
                        case EnumPersonType.Legal:
                            var resultInvalidCNPJ = InvalidCNPJ(customerValidateDTO.InputCreateCustomer.Document);
                            if (resultInvalidCNPJ != EnumValidateType.Valid)
                            {
                                customerValidateDTO.SetInvalid();
                                await InvalidGeneric(customerValidateDTO.InputCreateCustomer.Code, customerValidateDTO.InputCreateCustomer.Document, "CNPJ", resultInvalidCNPJ);
                            }

                            if (customerValidateDTO.InputCreateCustomer.BirthDate != null)
                            {
                                customerValidateDTO.SetInvalid();
                                await ManualNotification(customerValidateDTO.InputCreateCustomer.Code, "Pessoa jurídica não possui data de nascimento", EnumValidateType.Invalid);
                            }
                            break;
                        default:
                            customerValidateDTO.SetInvalid();
                            await InvalidGeneric(customerValidateDTO.InputCreateCustomer.Code, customerValidateDTO.InputCreateCustomer.PersonType, nameof(customerValidateDTO.InputCreateCustomer.PersonType), EnumValidateType.Invalid);
                            break;
                    }

                    if (!customerValidateDTO.Invalid)
                        await AddSuccessMessage(customerValidateDTO.InputCreateCustomer.Code, await GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Cliente"));
                }
                break;
            case EnumValidateProcessGeneric.Update:
                foreach (var customerValidateDTO in listCustomerValidateDTO)
                {
                    if (customerValidateDTO.InputIdentityUpdateCustomer == null)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    if (customerValidateDTO.InputIdentityUpdateCustomer.InputUpdate == null)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    if (customerValidateDTO.OriginalCustomerDTO == null)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    var repeatedInputUpdate = customerValidateDTO.ListRepeatedInputIdentityUpdateCustomer?.Count > 0;
                    if (repeatedInputUpdate)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    var resultFirstNameInvalidLength = InvalidLength(customerValidateDTO.InputIdentityUpdateCustomer.InputUpdate.FirstName, 1, 100);
                    if (resultFirstNameInvalidLength != EnumValidateType.Valid)
                    {
                        customerValidateDTO.SetInvalid();
                        await InvalidLength(customerValidateDTO.OriginalCustomerDTO.ExternalPropertiesDTO.Code, customerValidateDTO.InputIdentityUpdateCustomer.InputUpdate.FirstName, 1, 100, resultFirstNameInvalidLength, nameof(customerValidateDTO.InputCreateCustomer.FirstName));
                    }

                    var resultLastNameInvalidLength = InvalidLength(customerValidateDTO.InputIdentityUpdateCustomer.InputUpdate.LastName, 1, 100);
                    if (resultLastNameInvalidLength != EnumValidateType.Valid)
                    {
                        customerValidateDTO.SetInvalid();
                        await InvalidLength(customerValidateDTO.OriginalCustomerDTO.ExternalPropertiesDTO.Code, customerValidateDTO.InputIdentityUpdateCustomer.InputUpdate.LastName, 1, 100, resultLastNameInvalidLength, nameof(customerValidateDTO.InputCreateCustomer.LastName));
                    }

                    if (!customerValidateDTO.Invalid)
                        await AddSuccessMessage(customerValidateDTO.OriginalCustomerDTO.ExternalPropertiesDTO.Code, await GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Cliente"));
                }
                break;
            case EnumValidateProcessGeneric.Delete:
                foreach (var customerValidateDTO in listCustomerValidateDTO)
                {
                    if (customerValidateDTO.InputIdentityDeleteCustomer == null)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    if (customerValidateDTO.OriginalCustomerDTO == null)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    var repeatedInputDelete = customerValidateDTO.ListRepeatedInputIdentityDeleteCustomer?.Count > 0;
                    if (repeatedInputDelete)
                    {
                        customerValidateDTO.SetInvalid();
                        await Invalid(listCustomerValidateDTO.IndexOf(customerValidateDTO));
                        continue;
                    }

                    if (!customerValidateDTO.Invalid)
                        await AddSuccessMessage(customerValidateDTO.OriginalCustomerDTO.ExternalPropertiesDTO.Code, await GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Cliente"));
                }
                break;
        }
    }

    #region Create
    public override async Task<BaseResult<List<OutputCustomer?>>> Create(List<InputCreateCustomer> listInputCreateCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListIdentifier((from i in listInputCreateCustomer select new InputIdentifierCustomer(i.Code)).ToList());

        var listCreate = (from i in listInputCreateCustomer
                          select new
                          {
                              InputCreateCustomer = i,
                              ListRepeatedInputCreateCustomer = (from j in listInputCreateCustomer where listInputCreateCustomer.Count(x => x.Code == i.Code) > 1 select j).ToList(),
                              OriginalCustomerDTO = (from j in listOriginalCustomerDTO where j.ExternalPropertiesDTO.Code == i.Code select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = (from i in listCreate select new CustomerValidateDTO().ValidateCreate(i.InputCreateCustomer, i.ListRepeatedInputCreateCustomer, i.OriginalCustomerDTO)).ToList();
        await ValidateProcess(listCustomerValidateDTO, EnumValidateProcessGeneric.Create);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateCustomer.Count)
            return BaseResult<List<OutputCustomer?>>.Failure(errors);

        List<CustomerDTO> listCreateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select new CustomerDTO().Create(i.InputCreateCustomer!.SetProperty(nameof(i.InputCreateCustomer.Document), new string(i.InputCreateCustomer.Document.Where(char.IsDigit).ToArray())))).ToList();
        return BaseResult<List<OutputCustomer?>>.Success(FromDTOToOutput(await _repository.Create(listCreateCustomerDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputCustomer?>>> Update(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListId((from i in listInputIdentityUpdateCustomer select i.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateCustomer
                          select new
                          {
                              InputIdentityUpdateCustomer = i,
                              ListRepeatedInputIdentityUpdateCustomer = (from j in listInputIdentityUpdateCustomer where listInputIdentityUpdateCustomer.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalCustomerDTO = (from j in listOriginalCustomerDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = (from i in listUpdate select new CustomerValidateDTO().ValidateUpdate(i.InputIdentityUpdateCustomer, i.ListRepeatedInputIdentityUpdateCustomer, i.OriginalCustomerDTO)).ToList();
        await ValidateProcess(listCustomerValidateDTO, EnumValidateProcessGeneric.Update);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateCustomer.Count)
            return BaseResult<List<OutputCustomer?>>.Failure(errors);

        List<CustomerDTO> listUpdateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select i.OriginalCustomerDTO!.Update(i.InputIdentityUpdateCustomer!.InputUpdate!)).ToList();
        return BaseResult<List<OutputCustomer?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateCustomerDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListId((from i in listInputIdentityDeleteCustomer select i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteCustomer
                          select new
                          {
                              InputIdentityDeleteCustomer = i,
                              ListRepeatedInputIdentityDeleteCustomer = (from j in listInputIdentityDeleteCustomer where listInputIdentityDeleteCustomer.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalCustomerDTO = (from j in listOriginalCustomerDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = (from i in listDelete select new CustomerValidateDTO().ValidateDelete(i.InputIdentityDeleteCustomer, i.ListRepeatedInputIdentityDeleteCustomer, i.OriginalCustomerDTO)).ToList();
        await ValidateProcess(listCustomerValidateDTO, EnumValidateProcessGeneric.Delete);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteCustomer.Count)
            return BaseResult<bool>.Failure(errors);

        List<CustomerDTO> listDeletepdateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select i.OriginalCustomerDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateCustomerDTO), [.. successes, .. errors]);
    }
    #endregion
}