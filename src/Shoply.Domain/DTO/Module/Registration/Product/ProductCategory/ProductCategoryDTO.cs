using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;
using Shoply.Domain.Utils;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductCategoryDTO : BaseDTO<InputCreateProductCategory, InputUpdateProductCategory, OutputProductCategory, ProductCategoryDTO, InternalPropertiesProductCategoryDTO, ExternalPropertiesProductCategoryDTO, AuxiliaryPropertiesProductCategoryDTO>, IBaseDTO<ProductCategoryDTO, OutputProductCategory>
{
    public ProductCategoryDTO GetDTO(OutputProductCategory output)
    {
        return new ProductCategoryDTO
        {
            InternalPropertiesDTO = new InternalPropertiesProductCategoryDTO().SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesProductCategoryDTO(output.Code, output.Description),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductCategoryDTO(output.ListProduct.Cast<ProductDTO>()).SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }
    public OutputProductCategory GetOutput(ProductCategoryDTO dto)
    {
        return new OutputProductCategory(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, dto.AuxiliaryPropertiesDTO.ListProduct.Cast<OutputProduct>())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator ProductCategoryDTO?(OutputProductCategory output)
    {
        return output == null ? default : new ProductCategoryDTO().GetDTO(output);
    }

    public static implicit operator OutputProductCategory?(ProductCategoryDTO dto)
    {
        return dto == null ? default : dto.GetOutput(dto);
    }
}