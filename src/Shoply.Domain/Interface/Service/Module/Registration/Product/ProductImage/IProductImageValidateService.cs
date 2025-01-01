using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IProductImageValidateService : IBaseValidateService
{
    void Create(List<ProductImageValidateDTO> listProductImageValidateDTO);
    void Delete(List<ProductImageValidateDTO> listProductImageValidateDTO);
}