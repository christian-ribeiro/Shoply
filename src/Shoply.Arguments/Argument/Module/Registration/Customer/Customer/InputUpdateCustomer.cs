using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputUpdateCustomer(string firstName, string lastName) : BaseInputUpdate<InputUpdateCustomer>
{
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
}