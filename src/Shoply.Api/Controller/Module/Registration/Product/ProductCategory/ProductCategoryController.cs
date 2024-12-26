using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class ProductCategoryController(IProductCategoryService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<IProductCategoryService, IShoplyUnitOfWork, InputCreateProductCategory, InputUpdateProductCategory, InputIdentityUpdateProductCategory, InputIdentityDeleteProductCategory, InputIdentifierProductCategory, OutputProductCategory>(service, unitOfWork, userService) { }