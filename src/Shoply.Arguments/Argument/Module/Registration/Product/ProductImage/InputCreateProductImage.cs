using Microsoft.AspNetCore.Http;
using Shoply.Arguments.Argument.Base;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.Module.Registration;

public class InputCreateProductImage : BaseInputCreate<InputCreateProductImage>
{
    public IFormFile File { get; private set; }
    public long ProductId { get; private set; }

    public InputCreateProductImage() { }

    [JsonConstructor]
    public InputCreateProductImage(IFormFile file, long productId)
    {
        File = file;
        ProductId = productId;
    }
}