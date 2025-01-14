using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

[method: JsonConstructor]
public class InputCreateProductImage(string fileName, string contentType, long fileLength, byte[] file, long productId) : BaseInputCreate<InputCreateProductImage>
{
    public string FileName { get; private set; } = fileName;
    public string ContentType { get; private set; } = contentType;
    public long FileLength { get; set; } = fileLength;
    public byte[] File { get; private set; } = file;
    public long ProductId { get; private set; } = productId;
}