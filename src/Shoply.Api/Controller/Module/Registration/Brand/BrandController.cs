using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class BrandController(IBrandService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<IBrandService, IShoplyUnitOfWork, InputCreateBrand, InputUpdateBrand, InputIdentityUpdateBrand, InputIdentityDeleteBrand, InputIdentifierBrand, OutputBrand>(service, unitOfWork, userService) { }