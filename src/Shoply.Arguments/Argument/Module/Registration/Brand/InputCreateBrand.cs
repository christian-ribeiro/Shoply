using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateBrand(string code, string description) : BaseInputCreate<InputCreateBrand>
{
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
}