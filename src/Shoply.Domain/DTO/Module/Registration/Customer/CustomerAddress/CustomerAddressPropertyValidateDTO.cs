using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerAddressPropertyValidateDTO : BaseValidateDTO
{
    public List<InputIdentityUpdateCustomerAddress>? ListRepeatedInputIdentityUpdate { get; private set; }
    public List<InputIdentityDeleteCustomerAddress>? ListRepeatedInputIdentityDelete { get; private set; }
    public CustomerAddressDTO? OriginalCustomerAddressDTO { get; private set; }
    public CustomerDTO? RelatedCustomerDTO { get; private set; }

    public CustomerAddressPropertyValidateDTO ValidateCreate(CustomerDTO? relatedCustomerDTO)
    {
        RelatedCustomerDTO = relatedCustomerDTO;
        return this;
    }

    public CustomerAddressPropertyValidateDTO ValidateUpdate(List<InputIdentityUpdateCustomerAddress>? listRepeatedInputIdentityUpdate, CustomerAddressDTO originalCustomerAddressDTO)
    {
        ListRepeatedInputIdentityUpdate = listRepeatedInputIdentityUpdate;
        OriginalCustomerAddressDTO = originalCustomerAddressDTO;
        return this;
    }


    public CustomerAddressPropertyValidateDTO ValidateDelete(List<InputIdentityDeleteCustomerAddress>? listRepeatedInputIdentityDelete, CustomerAddressDTO originalCustomerAddressDTO)
    {
        ListRepeatedInputIdentityDelete = listRepeatedInputIdentityDelete;
        OriginalCustomerAddressDTO = originalCustomerAddressDTO;
        return this;
    }
}