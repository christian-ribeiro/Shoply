using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class AuxiliaryPropertiesProductImageDTO : BaseAuxiliaryPropertiesDTO<AuxiliaryPropertiesProductImageDTO>
{
    public ProductDTO? Product { get; private set; }

    public AuxiliaryPropertiesProductImageDTO() { }

    public AuxiliaryPropertiesProductImageDTO(ProductDTO? product)
    {
        Product = product;
    }
}