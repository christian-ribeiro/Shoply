using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductDTO : BaseDTO<InputCreateProduct, InputUpdateProduct, OutputProduct, ProductDTO, InternalPropertiesProductDTO, ExternalPropertiesProductDTO, AuxiliaryPropertiesProductDTO>, IBaseDTO<ProductDTO, OutputProduct>
{
    public ProductDTO? GetDTO(OutputProduct output)
    {
        return new ProductDTO
        {
            InternalPropertiesDTO = new InternalPropertiesProductDTO(output.Markup).SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesProductDTO(output.Code, output.Description, output.BarCode, output.CostValue, output.SaleValue, output.Status, output.ProductCategoryId, output.MeasureUnitId, output.BrandId),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductDTO(output.ProductCategory!, output.MeasureUnit!, output.Brand!, (from i in output.ListProductImage ?? new List<OutputProductImage>() select (ProductImageDTO)(dynamic)i).ToList()).SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }

    public OutputProduct? GetOutput(ProductDTO dto)
    {
        return new OutputProduct(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, dto.ExternalPropertiesDTO.BarCode, dto.ExternalPropertiesDTO.CostValue, dto.ExternalPropertiesDTO.SaleValue, dto.ExternalPropertiesDTO.Status, dto.ExternalPropertiesDTO.ProductCategoryId, dto.ExternalPropertiesDTO.MeasureUnitId, dto.ExternalPropertiesDTO.BrandId, dto.InternalPropertiesDTO.Markup, dto.AuxiliaryPropertiesDTO.ProductCategory!, dto.AuxiliaryPropertiesDTO.MeasureUnit!, dto.AuxiliaryPropertiesDTO.Brand!, (from i in dto.AuxiliaryPropertiesDTO.ListProductImage ?? new List<ProductImageDTO>() select (OutputProductImage)(dynamic)i).ToList())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator ProductDTO?(OutputProduct output)
    {
        return output == null ? default : new ProductDTO().GetDTO(output);
    }

    public static implicit operator OutputProduct?(ProductDTO dto)
    {
        return dto == null ? default : dto.GetOutput(dto);
    }
}