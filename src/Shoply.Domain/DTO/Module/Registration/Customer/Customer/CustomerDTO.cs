using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerDTO : BaseDTO<InputCreateCustomer, InputUpdateCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>, IBaseDTO<CustomerDTO, OutputCustomer>
{
    public CustomerDTO? GetDTO(OutputCustomer output)
    {
        throw new NotImplementedException();
    }

    public OutputCustomer? GetOutput(CustomerDTO dto)
    {
        throw new NotImplementedException();
    }
}