using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.Entity.Module.Registration;

public class Customer : BaseEntity<Customer, InputCreateCustomer, InputUpdateCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>
{
    public string Code { get; private set; } = String.Empty;
    public string FirstName { get; private set; } = String.Empty;
    public string LastName { get; private set; } = String.Empty;
    public DateTime? BirthDate { get; private set; }
    public string Document { get; private set; } = String.Empty;
    public EnumPersonType PersonType { get; private set; }

    public virtual List<CustomerAddress>? ListCustomerAddress { get; private set; }

    public Customer() { }

    public Customer(string code, string firstName, string lastName, DateTime? birthDate, string document, EnumPersonType personType, List<CustomerAddress>? listCustomerAddress)
    {
        Code = code;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Document = document;
        PersonType = personType;
        ListCustomerAddress = listCustomerAddress;
    }
}