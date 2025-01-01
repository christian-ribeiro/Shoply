using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerAddressValidateService(ITranslationService translationService) : BaseValidateService<CustomerAddressValidateDTO>(translationService), ICustomerAddressValidateService
{
    public void Create(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Update(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Delete(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO)
    {
        throw new NotImplementedException();
    }
}