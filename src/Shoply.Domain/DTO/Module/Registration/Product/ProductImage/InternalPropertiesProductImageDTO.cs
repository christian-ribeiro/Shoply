using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class InternalPropertiesProductImageDTO : BaseInternalPropertiesDTO<InternalPropertiesProductImageDTO> {
    public string ImageUrl { get; private set; }

    public InternalPropertiesProductImageDTO() { }

    public InternalPropertiesProductImageDTO(string imageUrl)
    {
        ImageUrl = imageUrl;
    }
}