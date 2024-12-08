using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerAddressService(ICustomerAddressRepository repository) : BaseService<ICustomerAddressRepository, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress, InputIdentityUpdateCustomerAddress, InputIdentityDeleteCustomerAddress, CustomerAddressValidateDTO, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO, EnumValidateProcessGeneric>(repository), ICustomerAddressService { }