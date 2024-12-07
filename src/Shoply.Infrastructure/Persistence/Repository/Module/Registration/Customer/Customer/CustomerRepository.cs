using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Infrastructure.Persistence.Context;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.Repository.Base;

namespace Shoply.Infrastructure.Persistence.Repository.Module.Registration;

public class CustomerRepository(ShoplyDbContext context) : BaseRepository<ShoplyDbContext, Customer, InputCreateCustomer, InputUpdateCustomer, InputIdentifierCustomer, OutputCustomer, CustomerDTO, InternalPropertiesCustomerDTO, ExternalPropertiesCustomerDTO, AuxiliaryPropertiesCustomerDTO>(context), ICustomerRepository { }