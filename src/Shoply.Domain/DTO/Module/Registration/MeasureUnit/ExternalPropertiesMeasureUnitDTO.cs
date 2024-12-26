using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class ExternalPropertiesMeasureUnitDTO : BaseExternalPropertiesDTO<ExternalPropertiesMeasureUnitDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }

    public ExternalPropertiesMeasureUnitDTO() { }

    public ExternalPropertiesMeasureUnitDTO(string code, string description)
    {
        Code = code;
        Description = description;
    }
}