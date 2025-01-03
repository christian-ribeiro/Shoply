using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class Customer : BaseEntity<Customer, InputCreateCustomer, InputUpdateCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>, IBaseEntity<Customer, CustomerDTO>
{
    public string Code { get; private set; } = String.Empty;
    public string FirstName { get; private set; } = String.Empty;
    public string LastName { get; private set; } = String.Empty;
    public DateOnly? BirthDate { get; private set; }
    public string Document { get; private set; } = String.Empty;
    public EnumPersonType PersonType { get; private set; }

    public virtual List<CustomerAddress>? ListCustomerAddress { get; private set; }

    public Customer() { }

    public Customer(string code, string firstName, string lastName, DateOnly? birthDate, string document, EnumPersonType personType, List<CustomerAddress>? listCustomerAddress)
    {
        Code = code;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Document = document;
        PersonType = personType;
        ListCustomerAddress = listCustomerAddress;
    }

    public CustomerDTO GetDTO(Customer entity)
    {
        return new CustomerDTO
        {
            InternalPropertiesDTO = new InternalPropertiesCustomerDTO().SetInternalData(entity.Id, entity.CreationDate, entity.ChangeDate, entity.CreationUserId, entity.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesCustomerDTO(entity.Code, entity.FirstName, entity.LastName, entity.BirthDate, entity.Document, entity.PersonType),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesCustomerDTO((from i in entity.ListCustomerAddress ?? new List<CustomerAddress>() select (CustomerAddressDTO)(dynamic)i).ToList()).SetInternalData(entity.CreationUser!, entity.ChangeUser!)
        };
    }

    public Customer GetEntity(CustomerDTO dto)
    {
        return new Customer(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.FirstName, dto.ExternalPropertiesDTO.LastName, dto.ExternalPropertiesDTO.BirthDate, dto.ExternalPropertiesDTO.Document, dto.ExternalPropertiesDTO.PersonType, (from i in dto.AuxiliaryPropertiesDTO.ListCustomerAddress ?? new List<CustomerAddressDTO>() select (CustomerAddress)(dynamic)i).ToList())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator CustomerDTO?(Customer entity)
    {
        return entity == null ? default : new Customer().GetDTO(entity);
    }

    public static implicit operator Customer?(CustomerDTO dto)
    {
        return dto == null ? default : new Customer().GetEntity(dto);
    }
}