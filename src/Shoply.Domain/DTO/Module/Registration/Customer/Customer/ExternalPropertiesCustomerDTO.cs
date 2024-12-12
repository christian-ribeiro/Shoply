using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesCustomerDTO : BaseExternalPropertiesDTO<ExternalPropertiesCustomerDTO>
{
    public string Code { get; private set; } = String.Empty;
    public string FirstName { get; private set; } = String.Empty;
    public string LastName { get; private set; } = String.Empty;
    public DateOnly? BirthDate { get; private set; }
    public string Document { get; private set; } = String.Empty;
    public EnumPersonType PersonType { get; private set; }

    public ExternalPropertiesCustomerDTO() { }

    public ExternalPropertiesCustomerDTO(string code, string firstName, string lastName, DateOnly? birthDate, string document, EnumPersonType personType)
    {
        Code = code;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        Document = document;
        PersonType = personType;
    }
}