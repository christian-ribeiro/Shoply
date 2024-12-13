using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class CustomerAddress : BaseEntity<CustomerAddress, InputCreateCustomerAddress, InputUpdateCustomerAddress, OutputCustomerAddress, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>
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
}