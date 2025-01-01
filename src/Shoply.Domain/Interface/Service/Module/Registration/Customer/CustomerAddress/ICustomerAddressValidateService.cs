using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface ICustomerAddressValidateService : IBaseValidateService
{
    void Create(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO);
    void Update(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO);
    void Delete(List<CustomerAddressValidateDTO> listCustomerAddressValidateDTO);
}