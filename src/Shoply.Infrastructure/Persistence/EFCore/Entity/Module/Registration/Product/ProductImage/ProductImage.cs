using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Infrastructure.Entity.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class ProductImage : BaseEntity<ProductImage, InputCreateProductImage, OutputProductImage, ProductImageDTO, InternalPropertiesProductImageDTO, ExternalPropertiesProductImageDTO, AuxiliaryPropertiesProductImageDTO>, IBaseEntity<ProductImage, ProductImageDTO>
{
    public string FileName { get; private set; }
    public decimal FileLength { get; private set; }
    public string ImageUrl { get; private set; }
    public long ProductId { get; private set; }

    public virtual Product? Product { get; private set; }

    [NotMapped]
    public override DateTime? ChangeDate { get => base.ChangeDate; set => base.ChangeDate = value; }
    [NotMapped]
    public override long? ChangeUserId { get => base.ChangeUserId; set => base.ChangeUserId = value; }
    [NotMapped]
    public override User? ChangeUser { get => base.ChangeUser; set => base.ChangeUser = value; }

    public ProductImage() { }

    public ProductImage(string fileName, decimal fileLength, string imageUrl, long productId, Product? product)
    {
        FileName = fileName;
        FileLength = fileLength;
        ImageUrl = imageUrl;
        ProductId = productId;
        Product = product;
    }

    public ProductImageDTO GetDTO(ProductImage entity)
    {
        return new ProductImageDTO
        {
            InternalPropertiesDTO = new InternalPropertiesProductImageDTO().SetInternalData(entity.Id, entity.CreationDate, entity.ChangeDate, entity.CreationUserId, entity.ChangeUserId),
            ExternalPropertiesDTO = new ExternalPropertiesProductImageDTO(entity.FileName, entity.FileLength, entity.ImageUrl, entity.ProductId),
            AuxiliaryPropertiesDTO = new AuxiliaryPropertiesProductImageDTO(entity.Product!).SetInternalData(entity.CreationUser!, entity.ChangeUser!)
        };
    }

    public ProductImage GetEntity(ProductImageDTO dto)
    {
        return new ProductImage(dto.ExternalPropertiesDTO.FileName, dto.ExternalPropertiesDTO.FileLength, dto.ExternalPropertiesDTO.ImageUrl, dto.ExternalPropertiesDTO.ProductId, dto.AuxiliaryPropertiesDTO.Product!)
            .SetInternalData(dto.InternalPropertiesDTO.Id, dto.InternalPropertiesDTO.CreationDate, dto.InternalPropertiesDTO.CreationUserId, dto.InternalPropertiesDTO.ChangeDate, dto.InternalPropertiesDTO.ChangeUserId, dto.AuxiliaryPropertiesDTO.CreationUser!, dto.AuxiliaryPropertiesDTO.ChangeUser!);
    }

    public static implicit operator ProductImageDTO?(ProductImage entity)
    {
        return entity == null ? default : new ProductImage().GetDTO(entity);
    }

    public static implicit operator ProductImage?(ProductImageDTO dto)
    {
        return dto == null ? default : new ProductImage().GetEntity(dto);
    }
}