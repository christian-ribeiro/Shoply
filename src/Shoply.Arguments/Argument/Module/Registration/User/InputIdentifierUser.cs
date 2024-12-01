using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentifierUser(string email) : BaseInputIdentifier<InputIdentifierUser>
{
    public string Email { get; private set; } = email;
}