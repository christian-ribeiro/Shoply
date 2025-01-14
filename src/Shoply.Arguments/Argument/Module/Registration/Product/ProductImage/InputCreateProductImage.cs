using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateProductImage(string fileName, decimal fileLength, long productId) : BaseInputCreate<InputCreateProductImage>
{
    public string FileName { get; private set; } = fileName;
    public decimal FileLength { get; private set; } = fileLength;
    public long ProductId { get; private set; } = productId;
}