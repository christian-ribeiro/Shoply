using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesBrandDTO : BaseExternalPropertiesDTO<ExternalPropertiesBrandDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }

    public ExternalPropertiesBrandDTO() { }

    public ExternalPropertiesBrandDTO(string code, string description)
    {
        Code = code;
        Description = description;
    }
}