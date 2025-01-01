using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerAddressValidateDTO : CustomerAddressPropertyValidateDTO
{
    public InputCreateCustomerAddress? InputCreate { get; private set; }
    public InputIdentityUpdateCustomerAddress? InputIdentityUpdate { get; private set; }
    public InputIdentityDeleteCustomerAddress? InputIdentityDelete { get; private set; }

    public CustomerAddressValidateDTO ValidateCreate(InputCreateCustomerAddress? inputCreate, CustomerDTO? relatedCustomerDTO)
    {
        InputCreate = inputCreate;
        ValidateCreate(relatedCustomerDTO);
        return this;
    }

    public CustomerAddressValidateDTO ValidateUpdate(InputIdentityUpdateCustomerAddress? inputIdentityUpdate, List<InputIdentityUpdateCustomerAddress>? listRepeatedInputIdentityUpdate, CustomerAddressDTO originalCustomerAddressDTO)
    {
        InputIdentityUpdate = inputIdentityUpdate;
        ValidateUpdate(listRepeatedInputIdentityUpdate, originalCustomerAddressDTO);
        return this;
    }

    public CustomerAddressValidateDTO ValidateDelete(InputIdentityDeleteCustomerAddress? inputIdentityDelete, List<InputIdentityDeleteCustomerAddress>? listRepeatedInputIdentityDelete, CustomerAddressDTO originalCustomerAddressDTO)
    {
        InputIdentityDelete = inputIdentityDelete;
        ValidateDelete(listRepeatedInputIdentityDelete, originalCustomerAddressDTO);
        return this;
    }
}