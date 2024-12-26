using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityDeleteProductCategory(long id) : BaseInputIdentityDelete<InputIdentityDeleteProductCategory>
{
    public long Id { get; private set; } = id;
}