using Shoply.Api.Controller.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Infrastructure.Persistence.EFCore.UnitOfWork.Interface;

namespace Shoply.Api.Controller.Module.Registration;

public class CustomerAddressController(ICustomerAddressService service, IShoplyUnitOfWork unitOfWork, IUserService userService) : BaseController<ICustomerAddressService, IShoplyUnitOfWork, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentityUpdateCustomerAddress, InputIdentityDeleteCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress>(service, unitOfWork, userService) { }
