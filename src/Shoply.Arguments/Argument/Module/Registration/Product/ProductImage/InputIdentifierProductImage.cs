using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputIdentifierProductImage(string fileName) : BaseInputIdentifier<InputIdentifierProductImage>
{
    public string FileName { get; private set; } = fileName;
}