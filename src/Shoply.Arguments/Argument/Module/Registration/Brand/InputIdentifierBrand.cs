using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentifierBrand(string code) : BaseInputIdentifier<InputIdentifierBrand>
{
    public string Code { get; private set; } = code;
}