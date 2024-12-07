using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerPropertyValidateDTO : BaseValidateDTO
{
    public List<InputCreateCustomer>? ListRepeatedInputCreateCustomer { get; private set; }
    public List<InputIdentityUpdateCustomer>? ListRepeatedInputIdentityUpdateCustomer { get; private set; }
    public List<InputIdentityDeleteCustomer>? ListRepeatedInputIdentityDeleteCustomer { get; private set; }
    public CustomerDTO? OriginalCustomerDTO { get; private set; }

    public CustomerPropertyValidateDTO ValidateCreate(List<InputCreateCustomer>? listRepeatedInputCreateCustomer, CustomerDTO? originalCustomerDTO)
    {
        ListRepeatedInputCreateCustomer = listRepeatedInputCreateCustomer;
        OriginalCustomerDTO = originalCustomerDTO;
        return this;
    }

    public CustomerPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateCustomer>? listRepeatedInputIdentityUpdateCustomer, CustomerDTO originalCustomerDTO)
    {
        ListRepeatedInputIdentityUpdateCustomer = listRepeatedInputIdentityUpdateCustomer;
        OriginalCustomerDTO = originalCustomerDTO;
        return this;
    }


    public CustomerPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteCustomer>? listRepeatedInputIdentityDeleteCustomer, CustomerDTO originalCustomerDTO)
    {
        ListRepeatedInputIdentityDeleteCustomer = listRepeatedInputIdentityDeleteCustomer;
        OriginalCustomerDTO = originalCustomerDTO;
        return this;
    }
}