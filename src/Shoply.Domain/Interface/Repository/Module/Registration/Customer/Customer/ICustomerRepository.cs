using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Base;

namespace Shoply.Domain.Interface.Repository.Module.Registration;

public interface ICustomerRepository : IBaseRepository<InputCreateCustomer, InputUpdateCustomer, InputIdentifierCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO> { }