using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class ProductCategoryValidateService(ITranslationService translationService) : BaseValidateService<ProductCategoryValidateDTO>(translationService), IProductCategoryValidateService
{
    public void Create(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Update(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO)
    {
        throw new NotImplementedException();
    }

    public void Delete(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO)
    {
        throw new NotImplementedException();
    }
}