using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class CustomerValidateService(ITranslationService translationService) : BaseValidateService<CustomerValidateDTO>(translationService), ICustomerValidateService
{
    public void Create(List<CustomerValidateDTO> listCustomerValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Update(List<CustomerValidateDTO> listCustomerValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Delete(List<CustomerValidateDTO> listCustomerValidateDTO)
    {
        throw new NotImplementedException();
    }
}