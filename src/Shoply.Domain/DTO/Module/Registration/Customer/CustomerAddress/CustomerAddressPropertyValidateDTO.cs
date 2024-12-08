using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerAddressPropertyValidateDTO : BaseValidateDTO
{
    public List<InputIdentityUpdateCustomerAddress>? ListRepeatedInputIdentityUpdateCustomerAddress { get; private set; }
    public List<InputIdentityDeleteCustomerAddress>? ListRepeatedInputIdentityDeleteCustomerAddress { get; private set; }
    public CustomerAddressDTO? OriginalCustomerAddressDTO { get; private set; }
    public CustomerDTO? RelatedCustomerDTO { get; private set; }

    public CustomerAddressPropertyValidateDTO ValidateCreate(CustomerDTO? relatedCustomerDTO)
    {
        RelatedCustomerDTO = relatedCustomerDTO;
        return this;
    }

    public CustomerAddressPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateCustomerAddress>? listRepeatedInputIdentityUpdateCustomerAddress, CustomerAddressDTO originalCustomerAddressDTO)
    {
        ListRepeatedInputIdentityUpdateCustomerAddress = listRepeatedInputIdentityUpdateCustomerAddress;
        OriginalCustomerAddressDTO = originalCustomerAddressDTO;
        return this;
    }


    public CustomerAddressPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteCustomerAddress>? listRepeatedInputIdentityDeleteCustomerAddress, CustomerAddressDTO originalCustomerAddressDTO)
    {
        ListRepeatedInputIdentityDeleteCustomerAddress = listRepeatedInputIdentityDeleteCustomerAddress;
        OriginalCustomerAddressDTO = originalCustomerAddressDTO;
        return this;
    }
}