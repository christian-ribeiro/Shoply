using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesProductImageDTO : BaseExternalPropertiesDTO<ExternalPropertiesProductImageDTO>
{
    public string FileName { get; private set; }
    public decimal FileLength { get; private set; }
    public string ImageUrl { get; private set; }
    public long ProductId { get; private set; }

    public ExternalPropertiesProductImageDTO() { }

    public ExternalPropertiesProductImageDTO(string fileName, decimal fileLength, string imageUrl, long productId)
    {
        FileName = fileName;
        FileLength = fileLength;
        ImageUrl = imageUrl;
        ProductId = productId;
    }
}