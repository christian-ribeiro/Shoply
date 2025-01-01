using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IProductValidateService : IBaseValidateService
{
    void Create(List<ProductValidateDTO> listProductValidateDTO);
    void Update(List<ProductValidateDTO> listProductValidateDTO);
    void Delete(List<ProductValidateDTO> listProductValidateDTO);
}