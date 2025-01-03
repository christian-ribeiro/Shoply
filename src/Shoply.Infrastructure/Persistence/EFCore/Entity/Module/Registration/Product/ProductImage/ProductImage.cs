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
        throw new NotImplementedException();
    }

    public ProductImage GetEntity(ProductImageDTO dto)
    {
        throw new NotImplementedException();
    }
}