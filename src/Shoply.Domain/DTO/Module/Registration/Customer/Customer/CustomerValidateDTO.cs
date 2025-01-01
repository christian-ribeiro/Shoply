using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerValidateDTO : CustomerPropertyValidateDTO
{
    public InputCreateCustomer? InputCreate { get; private set; }
    public InputIdentityUpdateCustomer? InputIdentityUpdate { get; private set; }
    public InputIdentityDeleteCustomer? InputIdentityDelete { get; private set; }

    public CustomerValidateDTO ValidateCreate(InputCreateCustomer? inputCreate, List<InputCreateCustomer>? listRepeatedInputCreateCustomer, CustomerDTO originalCustomerDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(listRepeatedInputCreateCustomer, originalCustomerDTO);
        return this;
    }

    public CustomerValidateDTO ValidateUpdate(InputIdentityUpdateCustomer? inputIdentityUpdate, List<InputIdentityUpdateCustomer>? listRepeatedInputIdentityUpdate, CustomerDTO originalCustomerDTO)
    {
        InputIdentityUpdate = inputIdentityUpdate;
        ValidateUpdate(listRepeatedInputIdentityUpdate, originalCustomerDTO);
        return this;
    }

    public CustomerValidateDTO ValidateDelete(InputIdentityDeleteCustomer? inputIdentityDelete, List<InputIdentityDeleteCustomer>? listRepeatedInputIdentityDelete, CustomerDTO originalCustomerDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalCustomerDTO);
        return this;
    }
}