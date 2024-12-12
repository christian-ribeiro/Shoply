using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Module.Registration;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputCustomer(string code, string firstName, string lastName, DateOnly? birthDate, string document, EnumPersonType personType, List<OutputCustomerAddress>? listCustomerAddress) : BaseOutput<OutputCustomer>
{
    public string Code { get; private set; } = code;
    public string FirstName { get; private set; } = firstName;
    public string LastName { get; private set; } = lastName;
    public DateOnly? BirthDate { get; private set; } = birthDate;
    public string Document { get; private set; } = document;
    public EnumPersonType PersonType { get; private set; } = personType;
    public List<OutputCustomerAddress>? ListCustomerAddress { get; private set; } = listCustomerAddress;
}