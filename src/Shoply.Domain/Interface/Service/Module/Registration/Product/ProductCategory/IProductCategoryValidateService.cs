using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IProductCategoryValidateService : IBaseValidateService
{
    void Create(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO);
    void Update(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO);
    void Delete(List<ProductCategoryValidateDTO> listProductCategoryValidateDTO);
}