using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputUpdateProductCategory(string description) : BaseInputUpdate<InputUpdateProductCategory>
{
    public string Description { get; private set; } = description;
}