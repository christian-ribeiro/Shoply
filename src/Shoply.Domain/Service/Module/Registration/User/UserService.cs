using Shoply.Application.Argument.Authentication;
using Shoply.Application.Interface.Service.Authentication;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Arguments.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Security.Encryption;

namespace Shoply.Domain.Service.Module.Registration;

public class UserService(IUserRepository repository, IJwtService jwtService) : BaseService<IUserRepository, InputCreateUser, InputUpdateUser, InputIdentifierUser, OutputUser, InputIdentityUpdateUser, InputIdentityDeleteUser, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO>(repository), IUserService
{
    public override async Task<BaseResult<List<OutputUser?>>> Create(List<InputCreateUser> listInputCreate)
    {
        Dictionary<string, List<string>> validate = [];
        List<UserDTO> listCreatedUser = (from i in listInputCreate
                                         let nonInformedEmail = string.IsNullOrEmpty(i.Email) && AddOnDictionary(validate, listInputCreate.IndexOf(i).ToString(), "E-mail não informado")
                                         where !string.IsNullOrEmpty(i.Email)
                                         let invalidEmail = i.Email.InvalidEmail() && AddOnDictionary(validate, i.Email, "E-mail inválido")
                                         let nonInformedName = i.Name.InvalidLength(1, 150) && AddOnDictionary(validate, i.Email, "Nome não informado")
                                         let nonInformedPassword = i.Password.InvalidLength(1, 150) && AddOnDictionary(validate, i.Email, "Senha não informada")
                                         let invalidPasswordMatch = i.Password.InvalidStringMatch(i.ConfirmPassword) && AddOnDictionary(validate, i.Email, "Senhas não conferem")
                                         where !validate.ContainsKey(i.Email)
                                         let encryptedPassword = EncryptService.Encrypt(i.Password)
                                         select new UserDTO().Create(new ExternalPropertiesUserDTO(i.Name, encryptedPassword, i.Email, EnumLanguage.Portuguese))).ToList();

        if (validate.Count > 0)
        {
            var listDetailedError = (from i in validate select new DetailedError(0, i.Key, i.Value)).ToList();
            return BaseResult<List<OutputUser?>>.Failure(listDetailedError);
        }

        return BaseResult<List<OutputUser?>>.Success(FromDTOToOutput(await _repository.Create(listCreatedUser))!);
    }

    public static bool AddOnDictionary(Dictionary<string, List<string>> dictionary, string key, string value)
    {
        if (!dictionary.ContainsKey(key))
            dictionary.Add(key, [value]);
        else
            dictionary[key].Add(value);

        return true;
    }

    public override async Task<BaseResult<List<OutputUser?>>> Update(List<InputIdentityUpdateUser> listInputIdentityUpdate)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId((from i in listInputIdentityUpdate select i.Id).ToList());

        List<UserDTO> listUpdatedUser = (from i in listInputIdentityUpdate
                                         let originalUserDTO = (from j in listOriginalUserDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault()
                                         where originalUserDTO != null
                                         select originalUserDTO.Update(i.InputUpdate!)).ToList();

        return BaseResult<List<OutputUser?>>.Success(FromDTOToOutput(await _repository.Update(listUpdatedUser))!);
    }

    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteUser> listInputIdentityDelete)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId((from i in listInputIdentityDelete select i.Id).ToList());
        return BaseResult<bool>.Success(await _repository.Delete(listOriginalUserDTO));
    }

    public async Task<BaseResult<OutputAuthenticateUser>> Authenticate(InputAuthenticateUser inputAuthenticateUser)
    {
        UserDTO? userDTO = await _repository.GetByIdentifier(new InputIdentifierUser(inputAuthenticateUser.Email));

        if (userDTO == null || !inputAuthenticateUser.Password.CompareHash(userDTO.ExternalPropertiesDTO.Password))
            return BaseResult<OutputAuthenticateUser>.Failure(new DetailedError(inputAuthenticateUser.Email, "Usuário ou senha inválidos"));

        string refreshToken = await jwtService.GenerateRefreshToken();

        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.RefreshToken), refreshToken);
        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.LoginKey), Guid.NewGuid());

        await _repository.Update(userDTO);

        string token = await jwtService.GenerateJwtToken(new JwtUser(userDTO.InternalPropertiesDTO.Id, userDTO.ExternalPropertiesDTO.Email, userDTO.ExternalPropertiesDTO.Name, userDTO.ExternalPropertiesDTO.Language));

        return BaseResult<OutputAuthenticateUser>.Success(new OutputAuthenticateUser(userDTO.InternalPropertiesDTO.Id, token));
    }
}