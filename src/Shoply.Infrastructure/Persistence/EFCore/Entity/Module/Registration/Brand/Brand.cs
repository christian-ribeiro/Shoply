using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Mapper;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;

public class Brand : BaseEntity<Brand, InputCreateBrand, InputUpdateBrand, OutputBrand, BrandDTO, InternalPropertiesBrandDTO, ExternalPropertiesBrandDTO, AuxiliaryPropertiesBrandDTO>, IBaseEntity<Brand, BrandDTO>
{
    public string Code { get; private set; }
    public string Description { get; private set; }

    public virtual List<Product>? ListProduct { get; private set; }

    public Brand() { }

    public Brand(string code, string description, List<Product>? listProduct)
    {
        Code = code;
        Description = description;
        ListProduct = listProduct;
    }

    public BrandDTO GetDTO(Brand entity)
    {
        throw new NotImplementedException();
    }

    public Brand GetEntity(BrandDTO dto)
    {
        throw new NotImplementedException();
    }
}