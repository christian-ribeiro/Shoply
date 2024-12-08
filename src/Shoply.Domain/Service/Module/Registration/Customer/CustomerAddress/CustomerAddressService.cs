using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerAddressService(ICustomerAddressRepository repository) : BaseService<ICustomerAddressRepository, InputCreateCustomerAddress, InputUpdateCustomerAddress, InputIdentifierCustomerAddress, OutputCustomerAddress, InputIdentityUpdateCustomerAddress, InputIdentityDeleteCustomerAddress, CustomerAddressValidateDTO, CustomerAddressDTO, InternalPropertiesCustomerAddressDTO, ExternalPropertiesCustomerAddressDTO, AuxiliaryPropertiesCustomerAddressDTO, EnumValidateProcessGeneric>(repository), ICustomerAddressService
{
    public override async Task<BaseResult<List<OutputCustomerAddress?>>> Create(List<InputCreateCustomerAddress> listInputCreate)
    {
        var teste = _repository.Teste();
        var teste2 = FromDTOToOutput(teste);
        var result = await _repository.GetDynamic(
            ["CustomerId", "PublicPlace", "Number", "Complement", "Neighborhood", "PostalCode", "Reference", "Observation", "Customer.Code",
            "Customer.FirstName", "Customer.LastName", "Customer.BirthDate", "Customer.Document", "Customer.PersonType"            ]);

        return default;
    }
}