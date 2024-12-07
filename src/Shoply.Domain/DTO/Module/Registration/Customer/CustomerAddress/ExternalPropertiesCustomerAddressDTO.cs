using Shoply.Arguments.Enum.Module;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesCustomerAddressDTO : BaseExternalPropertiesDTO<ExternalPropertiesCustomerAddressDTO>
{
    public long CustomerId { get; private set; }
    public EnumAddressType AddressType { get; private set; }
    public string PublicPlace { get; private set; } = String.Empty;
    public string Number { get; private set; } = String.Empty;
    public string? Complement { get; private set; } = String.Empty;
    public string Neighborhood { get; private set; } = String.Empty;
    public string PostalCode { get; private set; } = String.Empty;
    public string? Reference { get; private set; }
    public string? Observation { get; private set; }

    public ExternalPropertiesCustomerAddressDTO() { }

    public ExternalPropertiesCustomerAddressDTO(long customerId, EnumAddressType addressType, string publicPlace, string number, string? complement, string neighborhood, string postalCode, string? reference, string? observation)
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
    }
}