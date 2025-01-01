using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductImageValidateService(ITranslationService translationService) : BaseValidateService<ProductImageValidateDTO>(translationService), IProductImageValidateService
{
    public void Create(List<ProductImageValidateDTO> listProductImageValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Delete(List<ProductImageValidateDTO> listProductImageValidateDTO)
    {
        throw new NotImplementedException();
    }
}