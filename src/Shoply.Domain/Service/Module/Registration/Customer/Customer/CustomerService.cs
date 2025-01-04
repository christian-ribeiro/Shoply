using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerService(ICustomerRepository repository, ITranslationService translationService, ICustomerValidateService customerValidateService) : BaseService<ICustomerRepository, ICustomerValidateService, InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, InputIdentifierCustomer, OutputCustomer, CustomerValidateDTO, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>(repository, customerValidateService, translationService), ICustomerService
{
    #region Create
    public override async Task<BaseResult<List<OutputCustomer?>>> Create(List<InputCreateCustomer> listInputCreateCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListIdentifier(listInputCreateCustomer.Select(x => new InputIdentifierCustomer(x.Code)).ToList());

        var listCreate = (from i in listInputCreateCustomer
                          select new
                          {
                              InputCreateCustomer = i,
                              ListRepeatedInputCreateCustomer = listInputCreateCustomer.GetDuplicateItem(i, x => new { x.Code }),
                              OriginalCustomerDTO = listOriginalCustomerDTO.FirstOrDefault(x => x.ExternalPropertiesDTO.Code == i.Code),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = listCreate.Select(x => new CustomerValidateDTO().ValidateCreate(x.InputCreateCustomer, x.ListRepeatedInputCreateCustomer, x.OriginalCustomerDTO)).ToList();
        _validate.Create(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateCustomer.Count)
            return BaseResult<List<OutputCustomer?>>.Failure(errors);

        List<CustomerDTO> listCreateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select new CustomerDTO().Create(i.InputCreate!.SetProperty(x => x.Document, i.InputCreate.Document.GetOnlyDigit()))).ToList();
        return BaseResult<List<OutputCustomer?>>.Success(FromDTOToOutput(await _repository.Create(listCreateCustomerDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputCustomer?>>> Update(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListId(listInputIdentityUpdateCustomer.Select(x => x.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateCustomer
                          select new
                          {
                              InputIdentityUpdateCustomer = i,
                              ListRepeatedInputIdentityUpdateCustomer = listInputIdentityUpdateCustomer.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalCustomerDTO = listOriginalCustomerDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = listUpdate.Select(x => new CustomerValidateDTO().ValidateUpdate(x.InputIdentityUpdateCustomer, x.ListRepeatedInputIdentityUpdateCustomer, x.OriginalCustomerDTO)).ToList();
        _validate.Update(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateCustomer.Count)
            return BaseResult<List<OutputCustomer?>>.Failure(errors);

        List<CustomerDTO> listUpdateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select i.OriginalCustomerDTO!.Update(i.InputIdentityUpdate!.InputUpdate!)).ToList();
        return BaseResult<List<OutputCustomer?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateCustomerDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListId(listInputIdentityDeleteCustomer.Select(x => x.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteCustomer
                          select new
                          {
                              InputIdentityDeleteCustomer = i,
                              ListRepeatedInputIdentityDeleteCustomer = listInputIdentityDeleteCustomer.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalCustomerDTO = listOriginalCustomerDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = listDelete.Select(x => new CustomerValidateDTO().ValidateDelete(x.InputIdentityDeleteCustomer, x.ListRepeatedInputIdentityDeleteCustomer, x.OriginalCustomerDTO)).ToList();
        _validate.Delete(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteCustomer.Count)
            return BaseResult<bool>.Failure(errors);

        List<CustomerDTO> listDeletepdateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select i.OriginalCustomerDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateCustomerDTO), [.. successes, .. errors]);
    }
    #endregion
}