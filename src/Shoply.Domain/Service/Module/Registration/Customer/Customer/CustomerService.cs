using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerService(ICustomerRepository repository, ITranslationService translationService, ICustomerValidateService customerValidate) : BaseService<ICustomerRepository, InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer, InputIdentifierCustomer, OutputCustomer, CustomerValidateDTO, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>(repository, translationService), ICustomerService
{
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
        customerValidate.Create(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateCustomer.Count)
            return BaseResult<List<OutputCustomer?>>.Failure(errors);

        List<CustomerDTO> listCreateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select new CustomerDTO().Create(i.InputCreate!.SetProperty(nameof(i.InputCreate.Document), new string(i.InputCreate.Document.Where(char.IsDigit).ToArray())))).ToList();
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
        customerValidate.Update(listCustomerValidateDTO);

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
        List<CustomerDTO> listOriginalCustomerDTO = await _repository.GetListByListId((from i in listInputIdentityDeleteCustomer select i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteCustomer
                          select new
                          {
                              InputIdentityDeleteCustomer = i,
                              ListRepeatedInputIdentityDeleteCustomer = (from j in listInputIdentityDeleteCustomer where listInputIdentityDeleteCustomer.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalCustomerDTO = (from j in listOriginalCustomerDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<CustomerValidateDTO> listCustomerValidateDTO = (from i in listDelete select new CustomerValidateDTO().ValidateDelete(i.InputIdentityDeleteCustomer, i.ListRepeatedInputIdentityDeleteCustomer, i.OriginalCustomerDTO)).ToList();
        customerValidate.Delete(listCustomerValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteCustomer.Count)
            return BaseResult<bool>.Failure(errors);

        List<CustomerDTO> listDeletepdateCustomerDTO = (from i in RemoveInvalid(listCustomerValidateDTO) select i.OriginalCustomerDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateCustomerDTO), [.. successes, .. errors]);
    }
    #endregion
}