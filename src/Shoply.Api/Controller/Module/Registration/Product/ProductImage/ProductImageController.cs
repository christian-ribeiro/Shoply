using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class ProductImageController(IProductImageService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<IProductImageService, IShoplyUnitOfWork, InputCreateProductImage, InputIdentityDeleteProductImage, InputIdentifierProductImage, OutputProductImage>(service, unitOfWork, userService) { }