using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentityDeleteMeasureUnit(long id) : BaseInputIdentityDelete<InputIdentityDeleteMeasureUnit>
{
    public long Id { get; private set; } = id;
}