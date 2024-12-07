using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class AuxiliaryPropertiesCustomerAddressDTO : BaseAuxiliaryPropertiesDTO<AuxiliaryPropertiesCustomerAddressDTO>
{
    public CustomerDTO? Customer { get; private set; }

    public AuxiliaryPropertiesCustomerAddressDTO() { }

    public AuxiliaryPropertiesCustomerAddressDTO(CustomerDTO? customer)
    {
        Customer = customer;
    }
}