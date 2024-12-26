using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class ProductController(IProductService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<IProductService, IShoplyUnitOfWork, InputCreateProduct, InputUpdateProduct, InputIdentityUpdateProduct, InputIdentityDeleteProduct, InputIdentifierProduct, OutputProduct>(service, unitOfWork, userService) { }