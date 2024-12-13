using Shoply.Api.Controllers.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controllers.Module.Registration;

public class CustomerController(ICustomerService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<ICustomerService, IShoplyUnitOfWork, OutputCustomer, InputIdentifierCustomer, InputCreateCustomer, InputUpdateCustomer, InputIdentityUpdateCustomer, InputIdentityDeleteCustomer>(service, unitOfWork, userService) { }