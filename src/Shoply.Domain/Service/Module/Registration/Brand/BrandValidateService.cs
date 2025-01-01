using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class BrandValidateService(ITranslationService translationService) : BaseValidateService<BrandValidateDTO>(translationService), IBrandValidateService
{
    public void Create(List<BrandValidateDTO> listBrandValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Update(List<BrandValidateDTO> listBrandValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Delete(List<BrandValidateDTO> listBrandValidateDTO)
    {
        throw new NotImplementedException();
    }
}