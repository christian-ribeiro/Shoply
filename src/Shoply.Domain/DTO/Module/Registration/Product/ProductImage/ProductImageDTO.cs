using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;

namespace Shoply.Domain.DTO.Module.Registration;

public class ProductImageDTO : BaseDTO<InputCreateProductImage, OutputProductImage, ProductImageDTO, InternalPropertiesProductImageDTO, ExternalPropertiesProductImageDTO, AuxiliaryPropertiesProductImageDTO>, IBaseDTO<ProductImageDTO, OutputProductImage>
{
    public ProductImageDTO GetDTO(OutputProductImage output)
    {
        return new ProductImageDTO
        {
            InternalPropertiesDTO = new InternalPropertiesProductImageDTO().SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesProductImageDTO(output.FileName, output.FileLength, output.ImageUrl, output.ProductId),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductImageDTO(output.Product!).SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }

    public OutputProductImage GetOutput(ProductImageDTO dto)
    {
        return new OutputProductImage(dto.ExternalPropertiesDTO.FileName, dto.ExternalPropertiesDTO.FileLength, dto.ExternalPropertiesDTO.ImageUrl, dto.ExternalPropertiesDTO.ProductId, dto.AuxiliaryPropertiesDTO.Product!)
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator ProductImageDTO?(OutputProductImage output)
    {
        return output == null ? default : new ProductImageDTO().GetDTO(output);
    }

    public static implicit operator OutputProductImage?(ProductImageDTO dto)
    {
        return dto == null ? default : dto.GetOutput(dto);
    }

}