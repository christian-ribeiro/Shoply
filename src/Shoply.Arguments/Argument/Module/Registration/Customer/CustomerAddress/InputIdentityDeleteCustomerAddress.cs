using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityDeleteCustomerAddress(long id) : BaseInputIdentityDelete<InputIdentityDeleteCustomerAddress>
{
    public long Id { get; private set; } = id;
}