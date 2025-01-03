using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerAddressDTO : BaseDTO<InputCreateCustomerAddress, InputUpdateCustomerAddress, OutputCustomerAddress, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>, IBaseDTO<CustomerAddressDTO, OutputCustomerAddress>
{
    public CustomerAddressDTO? GetDTO(OutputCustomerAddress output)
    {
        throw new NotImplementedException();
    }

    public OutputCustomerAddress? GetOutput(CustomerAddressDTO dto)
    {
        throw new NotImplementedException();
    }
}