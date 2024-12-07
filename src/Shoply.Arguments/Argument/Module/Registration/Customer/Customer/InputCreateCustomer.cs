using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateCustomer(string code, string firstName, string lastName, DateTime? birthDate, string document, EnumPersonType personType) : BaseInputCreate<InputCreateCustomer>
{
    public string Code { get; private set; } = code;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public DateTime? BirthDate { get; private set; } = birthDate;
    public string Document { get; private set; } = document;
    public EnumPersonType PersonType { get; private set; } = personType;
}