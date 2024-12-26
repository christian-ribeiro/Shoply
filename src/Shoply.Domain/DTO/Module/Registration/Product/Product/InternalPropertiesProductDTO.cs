using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class InternalPropertiesProductDTO : BaseInternalPropertiesDTO<InternalPropertiesProductDTO>
{
    public decimal Markup { get; private set; }

    public InternalPropertiesProductDTO() { }

    public InternalPropertiesProductDTO(decimal markup)
    {
        Markup = markup;
    }
}