using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputUpdateBrand(string description) : BaseInputUpdate<InputUpdateBrand>
{
    public string Description { get; private set; } = description;
}