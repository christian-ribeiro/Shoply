using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateProductCategory(string code, string description) : BaseInputCreate<InputCreateProductCategory>
{
    public string Code { get; private set; } = code;
    public string Description { get; private set; } = description;
}