using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class AuxiliaryPropertiesCustomerDTO : BaseAuxiliaryPropertiesDTO<AuxiliaryPropertiesCustomerDTO>
{
    public List<CustomerAddressDTO>? ListCustomerAddress { get; private set; }

    public AuxiliaryPropertiesCustomerDTO() { }

    public AuxiliaryPropertiesCustomerDTO(List<CustomerAddressDTO>? listCustomerAddress)
    {
        ListCustomerAddress = listCustomerAddress;
    }
}