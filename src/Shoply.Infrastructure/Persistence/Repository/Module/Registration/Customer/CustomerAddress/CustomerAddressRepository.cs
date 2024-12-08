using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Infrastructure.Persistence.Context;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.Repository.Base;

namespace Shoply.Infrastructure.Persistence.Repository.Module.Registration;

public class CustomerAddressRepository(ShoplyDbContext context) : BaseRepository<ShoplyDbContext, CustomerAddress, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>(context), ICustomerAddressRepository { }