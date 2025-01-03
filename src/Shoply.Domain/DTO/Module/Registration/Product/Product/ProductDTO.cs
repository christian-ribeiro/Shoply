using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductDTO : BaseDTO<InputCreateProduct, InputUpdateProduct, OutputProduct, ProductDTO, InternalPropertiesProductDTO, ExternalPropertiesProductDTO, AuxiliaryPropertiesProductDTO>, IBaseDTO<ProductDTO, OutputProduct>
{
    public ProductDTO? GetDTO(OutputProduct output)
    {
        throw new NotImplementedException();
    }

    public OutputProduct? GetOutput(ProductDTO dto)
    {
        throw new NotImplementedException();
    }
}