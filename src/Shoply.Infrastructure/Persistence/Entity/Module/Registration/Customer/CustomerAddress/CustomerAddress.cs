using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.Entity.Module.Registration;

public class CustomerAddress : BaseEntity<CustomerAddress, InputCreateCustomerAddress, InputUpdateCustomerAddress, OutputCustomerAddress, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>
{
    public long CustomerId { get; set; }
    public EnumAddressType AddressType { get; set; }
    public string PublicPlace { get; set; } = String.Empty;
    public string Number { get; set; } = String.Empty;
    public string? Complement { get; set; }
    public string Neighborhood { get; set; } = String.Empty;
    public string PostalCode { get; set; } = String.Empty;
    public string? Reference { get; set; }
    public string? Observation { get; set; }

    public virtual Customer? Customer { get; set; }
    public virtual User? User { get; set; }

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
}