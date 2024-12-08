using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentifierCustomerAddress(long customerId, EnumAddressType addressType, string publicPlace) : BaseInputIdentifier<InputIdentifierCustomerAddress>
{
    public long CustomerId { get; private set; } = customerId;
    public EnumAddressType AddressType { get; private set; } = addressType;
    public string PublicPlace { get; set; } = publicPlace;
}