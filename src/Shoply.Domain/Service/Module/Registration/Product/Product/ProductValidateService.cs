using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductValidateService(ITranslationService translationService) : BaseValidateService<ProductValidateDTO>(translationService), IProductValidateService
{
    public void Create(List<ProductValidateDTO> listProductValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Update(List<ProductValidateDTO> listProductValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Delete(List<ProductValidateDTO> listProductValidateDTO)
    {
        throw new NotImplementedException();
    }
}