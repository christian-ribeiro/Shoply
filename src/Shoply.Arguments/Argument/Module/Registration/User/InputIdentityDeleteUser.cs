using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityDeleteUser(long id) : BaseInputIdentityDelete<InputIdentityDeleteUser>
{
    public long Id { get; private set; } = id;
}