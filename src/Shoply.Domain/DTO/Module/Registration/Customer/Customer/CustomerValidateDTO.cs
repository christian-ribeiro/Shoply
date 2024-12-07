using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerValidateDTO : CustomerPropertyValidateDTO
{
    public InputCreateCustomer? InputCreateCustomer { get; private set; }
    public InputIdentityUpdateCustomer? InputIdentityUpdateCustomer { get; private set; }
    public InputIdentityDeleteCustomer? InputIdentityDeleteCustomer { get; private set; }

    public CustomerValidateDTO ValidateCreate(InputCreateCustomer? inputCreateCustomer, List<InputCreateCustomer>? listRepeatedInputCreateCustomer, CustomerDTO originalCustomerDTO)
    {
        InputCreateCustomer = inputCreateCustomer;
        ValidateCreate(listRepeatedInputCreateCustomer, originalCustomerDTO);
        return this;
    }

    public CustomerValidateDTO ValidateUpdate(InputIdentityUpdateCustomer? inputIdentityUpdateCustomer, List<InputIdentityUpdateCustomer>? listRepeatedInputIdentityUpdateCustomer, CustomerDTO originalCustomerDTO)
    {
        InputIdentityUpdateCustomer = inputIdentityUpdateCustomer;
        ValidateUpdate(listRepeatedInputIdentityUpdateCustomer, originalCustomerDTO);
        return this;
    }

    public CustomerValidateDTO ValidateDelete(InputIdentityDeleteCustomer? inputIdentityDeleteCustomer, List<InputIdentityDeleteCustomer>? listRepeatedInputIdentityDeleteCustomer, CustomerDTO originalCustomerDTO)
    {
        InputIdentityDeleteCustomer = inputIdentityDeleteCustomer;
        ValidateDelete(listRepeatedInputIdentityDeleteCustomer, originalCustomerDTO);
        return this;
    }
}