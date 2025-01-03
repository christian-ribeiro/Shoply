using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductCategoryDTO : BaseDTO<InputCreateProductCategory, InputUpdateProductCategory, OutputProductCategory, ProductCategoryDTO, InternalPropertiesProductCategoryDTO, ExternalPropertiesProductCategoryDTO, AuxiliaryPropertiesProductCategoryDTO>, IBaseDTO<ProductCategoryDTO, OutputProductCategory>
{
    public ProductCategoryDTO? GetDTO(OutputProductCategory output)
    {
        throw new NotImplementedException();
    }

    public OutputProductCategory? GetOutput(ProductCategoryDTO dto)
    {
        throw new NotImplementedException();
    }
}