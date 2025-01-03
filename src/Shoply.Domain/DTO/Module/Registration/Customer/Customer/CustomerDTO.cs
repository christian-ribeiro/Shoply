using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerDTO : BaseDTO<InputCreateCustomer, InputUpdateCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>, IBaseDTO<CustomerDTO, OutputCustomer>
{
    public CustomerDTO? GetDTO(OutputCustomer output)
    {
        return new CustomerDTO
        {
            InternalPropertiesDTO = new InternalPropertiesCustomerDTO().SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesCustomerDTO(output.Code, output.FirstName, output.LastName, output.BirthDate, output.Document, output.PersonType),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerDTO((from i in output.ListCustomerAddress ?? new List<OutputCustomerAddress>() select (CustomerAddressDTO)(dynamic)i).ToList()).SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }
    public OutputCustomer? GetOutput(CustomerDTO dto)
    {
        return new OutputCustomer(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.FirstName, dto.ExternalPropertiesDTO.LastName, dto.ExternalPropertiesDTO.BirthDate, dto.ExternalPropertiesDTO.Document, dto.ExternalPropertiesDTO.PersonType, (from i in dto.AuxiliaryPropertiesDTO.ListCustomerAddress ?? new List<CustomerAddressDTO>() select (OutputCustomerAddress)(dynamic)i).ToList())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator CustomerDTO?(OutputCustomer output)
    {
        return output == null ? default : new CustomerDTO().GetDTO(output);
    }

    public static implicit operator OutputCustomer?(CustomerDTO dto)
    {
        return dto == null ? default : new CustomerDTO().GetOutput(dto);
    }

}