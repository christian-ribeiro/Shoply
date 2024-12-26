using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class AuxiliaryPropertiesMeasureUnitDTO : BaseAuxiliaryPropertiesDTO<AuxiliaryPropertiesMeasureUnitDTO>
{
    public List<ProductDTO>? ListProduct { get; private set; }

    public AuxiliaryPropertiesMeasureUnitDTO() { }

    public AuxiliaryPropertiesMeasureUnitDTO(List<ProductDTO>? listProduct)
    {
        ListProduct = listProduct;
    }
}