using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerAddressService(ICustomerAddressRepository repository, ITranslationService translationService, ICustomerAddressValidateService customerAddressValidate, ICustomerRepository customerRepository) : BaseService<ICustomerAddressRepository, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentityUpdateCustomerAddress, InputIdentityDeleteCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress, CustomerAddressValidateDTO, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>(repository, translationService), ICustomerAddressService
{
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
        customerAddressValidate.Create(listCustomerAddressValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateCustomerAddress.Count)
            return BaseResult<List<OutputCustomerAddress?>>.Failure(errors);

        List<CustomerAddressDTO> listCreateCustomerAddressDTO = (from i in RemoveInvalid(listCustomerAddressValidateDTO) select new CustomerAddressDTO().Create(i.InputCreate!)).ToList();
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
        customerAddressValidate.Update(listCustomerAddressValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateCustomerAddress.Count)
            return BaseResult<List<OutputCustomerAddress?>>.Failure(errors);

        List<CustomerAddressDTO> listUpdateCustomerAddressDTO = (from i in RemoveInvalid(listCustomerAddressValidateDTO) select i.OriginalCustomerAddressDTO!.Update(i.InputIdentityUpdate!.InputUpdate!)).ToList();
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
        customerAddressValidate.Delete(listCustomerAddressValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteCustomerAddress.Count)
            return BaseResult<bool>.Failure(errors);

        List<CustomerAddressDTO> listDeletepdateCustomerAddressDTO = (from i in RemoveInvalid(listCustomerAddressValidateDTO) select i.OriginalCustomerAddressDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateCustomerAddressDTO), [.. successes, .. errors]);
    }
    #endregion
}