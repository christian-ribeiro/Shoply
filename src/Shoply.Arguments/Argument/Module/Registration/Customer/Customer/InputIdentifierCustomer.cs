using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentifierCustomer(string code) : BaseInputIdentifier<InputIdentifierCustomer>
{
    public string Code { get; private set; } = code;
}