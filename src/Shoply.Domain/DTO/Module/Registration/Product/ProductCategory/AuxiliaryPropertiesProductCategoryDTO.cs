using Shoply.Domain.DTO.Base;

namespace Shoply.Domain.DTO.Module.Registration;

public class AuxiliaryPropertiesProductCategoryDTO : BaseAuxiliaryPropertiesDTO<AuxiliaryPropertiesProductCategoryDTO>
{
    public List<ProductDTO>? ListProduct { get; private set; }

    public AuxiliaryPropertiesProductCategoryDTO() { }

    public AuxiliaryPropertiesProductCategoryDTO(List<ProductDTO>? listProduct)
    {
        ListProduct = listProduct;
    }
}