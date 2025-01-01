using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IUserValidateService : IBaseValidateService
{
    void Create(List<UserValidateDTO> listUserValidateDTO);
    void Update(List<UserValidateDTO> listUserValidateDTO);
    void Delete(List<UserValidateDTO> listUserValidateDTO);
    void Authenticate(UserValidateDTO userValidateDTO);
    void RefreshToken(UserValidateDTO userValidateDTO);
    void SendEmailForgotPassword(UserValidateDTO userValidateDTO);
    void RedefinePasswordForgotPassword(UserValidateDTO userValidateDTO);
    void RedefinePassword(UserValidateDTO userValidateDTO);
}