using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityDeleteCustomer(long id) : BaseInputIdentityDelete<InputIdentityDeleteCustomer>
{
    public long Id { get; private set; } = id;
}