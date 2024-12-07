using Shoply.Arguments.Argument.Base;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputCustomer(string code, string firstName, string lastName, DateTime? birthDate, string document, EnumPersonType personType) : BaseOutput<OutputCustomer>
{
    public string Code { get; private set; } = code;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public DateTime? BirthDate { get; private set; } = birthDate;
    public string Document { get; private set; } = document;
    public EnumPersonType PersonType { get; private set; } = personType;
}