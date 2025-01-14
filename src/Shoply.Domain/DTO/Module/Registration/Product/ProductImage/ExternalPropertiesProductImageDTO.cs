using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesProductImageDTO : BaseExternalPropertiesDTO<ExternalPropertiesProductImageDTO>
{
    public string FileName { get; private set; }
    public long FileLength { get; private set; }
    public long ProductId { get; private set; }

    public ExternalPropertiesProductImageDTO() { }

    public ExternalPropertiesProductImageDTO(string fileName, long fileLength, long productId)
    {
        FileName = fileName;
        FileLength = fileLength;
        ProductId = productId;
    }
}