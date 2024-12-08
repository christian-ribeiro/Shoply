using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Infrastructure.Persistence.Context;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using Shoply.Infrastructure.Persistence.Repository.Base;
using System.Linq;

namespace Shoply.Infrastructure.Persistence.Repository.Module.Registration;

public class CustomerAddressRepository(ShoplyDbContext context) : BaseRepository<ShoplyDbContext, CustomerAddress, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO>(context), ICustomerAddressRepository
{
    public List<CustomerAddressDTO> Teste()
    {
        var result = _context.CustomerAddress.Select(x => new CustomerAddress
        {
            CustomerId = x.CustomerId,
            PublicPlace = x.PublicPlace,
            Number = x.Number,
            Complement = x.Complement,
            Neighborhood = x.Neighborhood,
            PostalCode = x.PostalCode,
            Reference = x.Reference,
            Observation = x.Observation,
            Customer = new Customer
            {
                Code = x.Customer.Code,
                FirstName = x.Customer.FirstName,
                LastName = x.Customer.LastName,
                BirthDate = x.Customer.BirthDate,
                Document = x.Customer.Document,
            }
        }).ToList();

        return FromEntityToDTO(result);
    }
}