using Shoply.Api.Controllers.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.UnitOfWork.Interface;

namespace Shoply.Api.Controllers.Module.Registration;

public class UserController(IUserService service, IShoplyUnitOfWork unitOfWork) : BaseController<IUserService, IShoplyUnitOfWork, OutputUser, InputIdentifierUser, InputCreateUser, InputUpdateUser, InputIdentityUpdateUser, InputIdentityDeleteUser>(service, unitOfWork) { }