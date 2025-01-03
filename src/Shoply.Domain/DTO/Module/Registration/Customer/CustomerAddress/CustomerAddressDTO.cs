using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class CustomerAddressDTO : BaseDTO<InputCreateCustomerAddress, InputUpdateCustomerAddress, OutputCustomerAddress, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>, IBaseDTO<CustomerAddressDTO, OutputCustomerAddress>
{
    public CustomerAddressDTO GetDTO(OutputCustomerAddress output)
    {
        return new CustomerAddressDTO
        {
            InternalPropertiesDTO = new InternalPropertiesCustomerAddressDTO().SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesCustomerAddressDTO(output.CustomerId, output.AddressType, output.PublicPlace, output.Number, output.Complement, output.Neighborhood, output.PostalCode, output.Reference, output.Observation),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerAddressDTO(output.Customer!).SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }
    public OutputCustomerAddress GetOutput(CustomerAddressDTO dto)
    {
        return new OutputCustomerAddress(dto.ExternalPropertiesDTO.CustomerId, dto.ExternalPropertiesDTO.AddressType, dto.ExternalPropertiesDTO.PublicPlace, dto.ExternalPropertiesDTO.Number, dto.ExternalPropertiesDTO.Complement, dto.ExternalPropertiesDTO.Neighborhood, dto.ExternalPropertiesDTO.PostalCode, dto.ExternalPropertiesDTO.Reference, dto.ExternalPropertiesDTO.Observation, dto.AuxiliaryPropertiesDTO.Customer!)
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator CustomerAddressDTO?(OutputCustomerAddress output)
    {
        return output == null ? default : new CustomerAddressDTO().GetDTO(output);
    }

    public static implicit operator OutputCustomerAddress?(CustomerAddressDTO dto)
    {
        return dto == null ? default : dto.GetOutput(dto);
    }
}