using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.Context;
using Shoply.Infrastructure.Persistence.EFCore.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.Repository.Base;

namespace Shoply.Infrastructure.Persistence.EFCore.Repository.Module.Registration;

public class ProductRepository(ShoplyDbContext context) : BaseRepository<ShoplyDbContext, Product, InputCreateProduct, InputUpdateProduct, InputIdentifierProduct, OutputProduct, ProductDTO, InternalPropertiesProductDTO, ExternalPropertiesProductDTO, AuxiliaryPropertiesProductDTO>(context), IProductRepository { }