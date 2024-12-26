using Shoply.Arguments.Argument.Base;

namespace Shoply.Arguments.Argument.Module.Registration;

public class OutputProductImage(string fileName, decimal fileLength, string imageUrl, long productId, OutputProduct? product) : BaseOutput<OutputProductImage>
{
    public string FileName { get; private set; } = fileName;
    public decimal FileLength { get; private set; } = fileLength;
    public string ImageUrl { get; private set; } = imageUrl;
    public long ProductId { get; private set; } = productId;
    public OutputProduct? Product { get; private set; } = product;
}