using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface ICustomerValidateService : IBaseValidateService
{
    void Create(List<CustomerValidateDTO> listCustomerValidateDTO);
    void Update(List<CustomerValidateDTO> listCustomerValidateDTO);
    void Delete(List<CustomerValidateDTO> listCustomerValidateDTO);
}