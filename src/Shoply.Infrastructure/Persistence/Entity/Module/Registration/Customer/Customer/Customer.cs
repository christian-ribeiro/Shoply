using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.Entity.Module.Registration;

public class Customer(string code, string firstName, string lastName, DateTime? birthDate, string document, EnumPersonType personType) : BaseEntity<Customer, InputCreateCustomer, InputUpdateCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>
{
    public string Code { get; private set; } = code;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public DateTime? BirthDate { get; private set; } = birthDate;
    public string Document { get; private set; } = document;
    public EnumPersonType PersonType { get; private set; } = personType;
}