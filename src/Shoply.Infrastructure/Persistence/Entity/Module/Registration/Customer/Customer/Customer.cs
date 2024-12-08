using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.Entity.Module.Registration;

public class Customer : BaseEntity<Customer, InputCreateCustomer, InputUpdateCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>
{
    public string Code { get; set; } = String.Empty;
    public string FirstName { get; set; } = String.Empty;
    public string LastName { get; set; } = String.Empty;
    public DateTime? BirthDate { get; set; }
    public string Document { get; set; } = String.Empty;
    public EnumPersonType PersonType { get; set; }

    public virtual List<CustomerAddress>? ListCustomerAddress { get; set; }

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