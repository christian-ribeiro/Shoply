using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class AuxiliaryPropertiesBrandDTO : BaseAuxiliaryPropertiesDTO<AuxiliaryPropertiesBrandDTO>
{
    public List<ProductDTO>? ListProduct { get; private set; }

    public AuxiliaryPropertiesBrandDTO() { }

    public AuxiliaryPropertiesBrandDTO(List<ProductDTO>? listProduct)
    {
        ListProduct = listProduct;
    }
}