using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface ICustomerAddressService : IBaseService<InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress, InputIdentityUpdateCustomerAddress, InputIdentityDeleteCustomerAddress> { }