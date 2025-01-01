using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Base;

namespace Shoply.Domain.Interface.Service.Module.Registration;

public interface IUserValidateService : IBaseValidateService
{
    void ValidateCreate(List<UserValidateDTO> listUserValidateDTO);
    void ValidateUpdate(List<UserValidateDTO> listUserValidateDTO);
    void ValidateDelete(List<UserValidateDTO> listUserValidateDTO);
    void ValidateAuthenticate(UserValidateDTO userValidateDTO);
    void ValidateRefreshToken(UserValidateDTO userValidateDTO);
    void ValidateSendEmailForgotPassword(UserValidateDTO userValidateDTO);
    void ValidateRedefinePasswordForgotPassword(UserValidateDTO userValidateDTO);
    void ValidateRedefinePassword(UserValidateDTO userValidateDTO);
}