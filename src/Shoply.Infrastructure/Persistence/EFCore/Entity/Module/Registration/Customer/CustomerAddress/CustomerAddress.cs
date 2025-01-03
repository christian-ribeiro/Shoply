using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class CustomerAddress : BaseEntity<CustomerAddress, InputCreateCustomerAddress, InputUpdateCustomerAddress, OutputCustomerAddress, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>, IBaseEntity<CustomerAddress, CustomerAddressDTO>
{
    public long CustomerId { get; private set; }
    public EnumAddressType AddressType { get; private set; }
    public string PublicPlace { get; private set; } = String.Empty;
    public string Number { get; private set; } = String.Empty;
    public string? Complement { get; private set; }
    public string Neighborhood { get; private set; } = String.Empty;
    public string PostalCode { get; private set; } = String.Empty;
    public string? Reference { get; private set; }
    public string? Observation { get; private set; }

    public virtual Customer? Customer { get; private set; }

    public CustomerAddress() { }

    public CustomerAddress(long customerId, EnumAddressType addressType, string publicPlace, string number, string? complement, string neighborhood, string postalCode, string? reference, string? observation, Customer? customer)
    {
        CustomerId = customerId;
        AddressType = addressType;
        PublicPlace = publicPlace;
        Number = number;
        Complement = complement;
        Neighborhood = neighborhood;
        PostalCode = postalCode;
        Reference = reference;
        Observation = observation;
        Customer = customer;
    }

    public CustomerAddressDTO GetDTO(CustomerAddress entity)
    {
        return new CustomerAddressDTO
        {
            InternalPropertiesDTO = new InternalPropertiesCustomerAddressDTO().SetInternalData(entity.Id, entity.CreationDate, entity.ChangeDate, entity.CreationUserId, entity.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesCustomerAddressDTO(entity.CustomerId, entity.AddressType, entity.PublicPlace, entity.Number, entity.Complement, entity.Neighborhood, entity.PostalCode, entity.Reference, entity.Observation),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerAddressDTO(entity.Customer!).SetInternalData(entity.CreationUser!, entity.ChangeUser!)
        };
    }

    public CustomerAddress GetEntity(CustomerAddressDTO dto)
    {
        return new CustomerAddress(dto.ExternalPropertiesDTO.CustomerId, dto.ExternalPropertiesDTO.AddressType, dto.ExternalPropertiesDTO.PublicPlace, dto.ExternalPropertiesDTO.Number, dto.ExternalPropertiesDTO.Complement, dto.ExternalPropertiesDTO.Neighborhood, dto.ExternalPropertiesDTO.PostalCode, dto.ExternalPropertiesDTO.Reference, dto.ExternalPropertiesDTO.Observation, dto.AuxiliaryPropertiesDTO.Customer!)
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator CustomerAddressDTO?(CustomerAddress entity)
    {
        return entity == null ? default : new CustomerAddress().GetDTO(entity);
    }

    public static implicit operator CustomerAddress?(CustomerAddressDTO dto)
    {
        return dto == null ? default : new CustomerAddress().GetEntity(dto);
    }
}