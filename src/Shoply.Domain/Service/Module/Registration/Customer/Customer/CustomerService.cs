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
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListIdentifier([.. (from i in listInputCreateCustomer select new InputIdentifierCustomer(i.Code))]);

        var listCreate = (from i in listInputCreateCustomer
                          select new
                          {
                              InputCreateCustomer = i,
                              ListRepeatedInputCreateCustomer = listInputCreateCustomer.GetDuplicateItem(i, x => new { x.Code }),
                              OriginalCustomerDTO = (from j in listOriginalCustomerDTO where j.ExternalPropertiesDTO.Code == i.Code select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = [.. (from i in listCreate select new CustomerValidateDTO().ValidateCreate(i.InputCreateCustomer, i.ListRepeatedInputCreateCustomer, i.OriginalCustomerDTO))];
        _validate.Create(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateCustomer.Count)
            return BaseResult<List<OutputCustomer?>>.Failure(errors);

        List<CustomerDTO> listCreateCustomerDTO = [.. (from i in RemoveInvalid(listCustomerValidateDTO) select new CustomerDTO().Create(i.InputCreate!.SetProperty(x => x.Document, i.InputCreate.Document.GetOnlyDigit())))];
        return BaseResult<List<OutputCustomer?>>.Success(FromDTOToOutput(await _repository.Create(listCreateCustomerDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputCustomer?>>> Update(List<InputIdentityUpdateCustomer> listInputIdentityUpdateCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListId([.. (from i in listInputIdentityUpdateCustomer select i.Id)]);

        var listUpdate = (from i in listInputIdentityUpdateCustomer
                          select new
                          {
                              InputIdentityUpdateCustomer = i,
                              ListRepeatedInputIdentityUpdateCustomer = listInputIdentityUpdateCustomer.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalCustomerDTO = (from j in listOriginalCustomerDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = [.. (from i in listUpdate select new CustomerValidateDTO().ValidateUpdate(i.InputIdentityUpdateCustomer, i.ListRepeatedInputIdentityUpdateCustomer, i.OriginalCustomerDTO))];
        _validate.Update(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateCustomer.Count)
            return BaseResult<List<OutputCustomer?>>.Failure(errors);

        List<CustomerDTO> listUpdateCustomerDTO = [.. (from i in RemoveInvalid(listCustomerValidateDTO) select i.OriginalCustomerDTO!.Update(i.InputIdentityUpdate!.InputUpdate!))];
        return BaseResult<List<OutputCustomer?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateCustomerDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteCustomer> listInputIdentityDeleteCustomer)
    {
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListId([.. (from i in listInputIdentityDeleteCustomer select i.Id)]);

        var listDelete = (from i in listInputIdentityDeleteCustomer
                          select new
                          {
                              InputIdentityDeleteCustomer = i,
                              ListRepeatedInputIdentityDeleteCustomer = listInputIdentityDeleteCustomer.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalCustomerDTO = (from j in listOriginalCustomerDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = [.. (from i in listDelete select new CustomerValidateDTO().ValidateDelete(i.InputIdentityDeleteCustomer, i.ListRepeatedInputIdentityDeleteCustomer, i.OriginalCustomerDTO))];
        _validate.Delete(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteCustomer.Count)
            return BaseResult<bool>.Failure(errors);

        List<CustomerDTO> listDeletepdateCustomerDTO = [.. (from i in RemoveInvalid(listCustomerValidateDTO) select i.OriginalCustomerDTO)];
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateCustomerDTO), [.. successes, .. errors]);
    }
    #endregion
}