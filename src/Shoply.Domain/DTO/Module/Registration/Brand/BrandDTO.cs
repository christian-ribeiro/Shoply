using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Mapper;
using Shoply.Domain.Utils;

namespace Shoply.Domain.DTO.Module.Registration;

public class BrandDTO : BaseDTO<InputCreateBrand, InputUpdateBrand, OutputBrand, BrandDTO, InternalPropertiesBrandDTO, ExternalPropertiesBrandDTO, AuxiliaryPropertiesBrandDTO>, IBaseDTO<BrandDTO, OutputBrand>
{
    public BrandDTO GetDTO(OutputBrand output)
    {
        return new BrandDTO
        {
            InternalPropertiesDTO = new InternalPropertiesBrandDTO().SetInternalData(output.Id, output.CreationDate, output.ChangeDate, output.CreationUserId, output.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesBrandDTO(output.Code, output.Description),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesBrandDTO(output.ListProduct.Cast<ProductDTO>()).SetInternalData(output.CreationUser!, output.ChangeUser!)
        };
    }

    public OutputBrand GetOutput(BrandDTO dto)
    {
        return new OutputBrand(dto.ExternalPropertiesDTO.Code, dto.ExternalPropertiesDTO.Description, dto.AuxiliaryPropertiesDTO.ListProduct.Cast<OutputProduct>())
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator BrandDTO?(OutputBrand output)
    {
        return output == null ? default : new BrandDTO().GetDTO(output);
    }

    public static implicit operator OutputBrand?(BrandDTO dto)
    {
        return dto == null ? default : dto.GetOutput(dto);
    }
}