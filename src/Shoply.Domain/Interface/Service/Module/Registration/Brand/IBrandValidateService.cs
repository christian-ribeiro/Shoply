using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IBrandValidateService : IBaseValidateService
{
    void Create(List<BrandValidateDTO> listBrandValidateDTO);
    void Update(List<BrandValidateDTO> listBrandValidateDTO);
    void Delete(List<BrandValidateDTO> listBrandValidateDTO);
}