using Shoply.Arguments.Argument.Base;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputUpdateUser(string name) : BaseInputUpdate<InputUpdateUser>
{
    [Required]
    public string Name { get; private set; } = name;
}