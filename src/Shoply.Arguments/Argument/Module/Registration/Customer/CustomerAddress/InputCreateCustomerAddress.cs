using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateCustomerAddress(long customerId, EnumAddressType addressType, string publicPlace, string number, string? complement, string neighborhood, string postalCode, string? reference, string? observation) : BaseInputCreate<InputCreateCustomerAddress>
{
    public long CustomerId { get; private set; } = customerId;
    public EnumAddressType AddressType { get; private set; } = addressType;
    public string PublicPlace { get; set; } = publicPlace;
    public string Number { get; set; } = number;
    public string? Complement { get; set; } = complement;
    public string Neighborhood { get; set; } = neighborhood;
    public string PostalCode { get; set; } = postalCode;
    public string? Reference { get; set; } = reference;
    public string? Observation { get; set; } = observation;
}