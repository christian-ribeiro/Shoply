using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerAddressValidateDTO : CustomerAddressPropertyValidateDTO
{
    public InputCreateCustomerAddress? InputCreateCustomerAddress { get; private set; }
    public InputIdentityUpdateCustomerAddress? InputIdentityUpdateCustomerAddress { get; private set; }
    public InputIdentityDeleteCustomerAddress? InputIdentityDeleteCustomerAddress { get; private set; }

    public CustomerAddressValidateDTO ValidateCreate(InputCreateCustomerAddress? inputCreateCustomerAddress, CustomerDTO? relatedCustomerDTO)
    {
        InputCreateCustomerAddress = inputCreateCustomerAddress;
        ValidateCreate(relatedCustomerDTO);
        return this;
    }

    public CustomerAddressValidateDTO ValidateUpdate(InputIdentityUpdateCustomerAddress? inputIdentityUpdateCustomerAddress, List<InputIdentityUpdateCustomerAddress>? listRepeatedInputIdentityUpdateCustomerAddress, CustomerAddressDTO originalCustomerAddressDTO)
    {
        InputIdentityUpdateCustomerAddress = inputIdentityUpdateCustomerAddress;
        ValidateUpdate(listRepeatedInputIdentityUpdateCustomerAddress, originalCustomerAddressDTO);
        return this;
    }

    public CustomerAddressValidateDTO ValidateDelete(InputIdentityDeleteCustomerAddress? inputIdentityDeleteCustomerAddress, List<InputIdentityDeleteCustomerAddress>? listRepeatedInputIdentityDeleteCustomerAddress, CustomerAddressDTO originalCustomerAddressDTO)
    {
        InputIdentityDeleteCustomerAddress = inputIdentityDeleteCustomerAddress;
        ValidateDelete(listRepeatedInputIdentityDeleteCustomerAddress, originalCustomerAddressDTO);
        return this;
    }
}