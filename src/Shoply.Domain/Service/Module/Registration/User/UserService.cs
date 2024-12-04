using Shoply.Application.Argument.Authentication;
using Shoply.Application.Interface.Service.Authentication;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Security.Encryption;

namespace Shoply.Domain.Service.Module.Registration;

public class UserService(IUserRepository repository, IJwtService jwtService) : BaseService<IUserRepository, InputCreateUser, InputUpdateUser, InputIdentifierUser, OutputUser, InputIdentityUpdateUser, InputIdentityDeleteUser, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO>(repository), IUserService
{
    public override async Task<List<OutputUser?>> Create(List<InputCreateUser> listInputCreate)
    {
        List<UserDTO> listCreatedUser = (from i in listInputCreate
                                         let encryptedPassword = EncryptService.Encrypt(i.Password)
                                         select new UserDTO().Create(new ExternalPropertiesUserDTO(i.Name, encryptedPassword, i.Email, EnumLanguage.Portuguese))).ToList();
        return FromDTOToOutput(await _repository.Create(listCreatedUser));
    }

    public override async Task<List<OutputUser?>> Update(List<InputIdentityUpdateUser> listInputIdentityUpdate)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId((from i in listInputIdentityUpdate select i.Id).ToList());

        List<UserDTO> listUpdatedUser = (from i in listInputIdentityUpdate
                                         let originalUserDTO = (from j in listOriginalUserDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault()
                                         where originalUserDTO != null
                                         select originalUserDTO.Update(i.InputUpdate!)).ToList();

        return FromDTOToOutput(await _repository.Update(listUpdatedUser));
    }

    public override async Task<bool> Delete(List<InputIdentityDeleteUser> listInputIdentityDelete)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId((from i in listInputIdentityDelete select i.Id).ToList());
        return await _repository.Delete(listOriginalUserDTO);
    }

    public async Task<string> Authenticate(InputAuthenticateUser inputAuthenticateUser)
    {
        UserDTO? userDTO = await _repository.GetByIdentifier(new InputIdentifierUser(inputAuthenticateUser.Email));

        if (userDTO == null || !inputAuthenticateUser.Password.CompareHash(userDTO.ExternalPropertiesDTO.Password))
            return "Usuário ou senha inválidos";

        string refreshToken = await jwtService.GenerateRefreshToken();

        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.RefreshToken), refreshToken);
        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.LoginKey), Guid.NewGuid());

        await _repository.Update(userDTO);

        return await jwtService.GenerateJwtToken(new InputJwtUser(userDTO.InternalPropertiesDTO.Id, userDTO.ExternalPropertiesDTO.Email, userDTO.ExternalPropertiesDTO.Name, userDTO.ExternalPropertiesDTO.Language));
    }
}