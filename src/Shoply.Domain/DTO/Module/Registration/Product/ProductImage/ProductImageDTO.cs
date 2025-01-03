using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductImageDTO : BaseDTO<InputCreateProductImage, OutputProductImage, ProductImageDTO, InternalPropertiesProductImageDTO, ExternalPropertiesProductImageDTO, AuxiliaryPropertiesProductImageDTO>, IBaseDTO<ProductImageDTO, OutputProductImage>
{
    public ProductImageDTO? GetDTO(OutputProductImage output)
    {
        throw new NotImplementedException();
    }

    public OutputProductImage? GetOutput(ProductImageDTO dto)
    {
        throw new NotImplementedException();
    }
}