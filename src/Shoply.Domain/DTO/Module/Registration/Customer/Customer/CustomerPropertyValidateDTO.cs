using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateCustomer>? ListRepeatedInputCreate { get; private set; }
    public List<InputIdentityUpdateCustomer>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteCustomer>? ListRepeatedInputIdentityDelete { get; private set; }
    public CustomerDTO? OriginalCustomerDTO { get; private set; }

    public CustomerPropertyValidateDTO ValidateCreate(List<InputCreateCustomer>? listRepeatedInputCreate, CustomerDTO? originalCustomerDTO)
    {
        ListRepeatedInputCreate = listRepeatedInputCreate;
        OriginalCustomerDTO = originalCustomerDTO;
        return this;
    }

    public CustomerPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateCustomer>? listRepeatedInputIdentityUpdate, CustomerDTO originalCustomerDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdate;
        OriginalCustomerDTO = originalCustomerDTO;
        return this;
    }


    public CustomerPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteCustomer>? listRepeatedInputIdentityDelete, CustomerDTO originalCustomerDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalCustomerDTO = originalCustomerDTO;
        return this;
    }
}