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
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListIdentifier((from i in listInputCreate select new InputIdentifierUser(i.Email)).ToList());

        Dictionary<string, List<string>> validate = [];
        List<UserDTO> listCreatedUser = (from i in listInputCreate.Index()
                                         let listRepeatedInputCreate = (from j in listInputCreate.Index() where j.Item.Email == i.Item.Email where j.Index != i.Index select j).Any() && AddOnDictionary(validate, i.Index.ToString(), $"{i.Item.Email} repetido")
                                         where !listRepeatedInputCreate
                                         let nonInformedEmail = string.IsNullOrEmpty(i.Item.Email) && AddOnDictionary(validate, i.Index.ToString(), "E-mail não informado")
                                         where !nonInformedEmail
                                         let originalUserDTO = (from j in listOriginalUserDTO where j.ExternalPropertiesDTO.Email == i.Item.Email select j).FirstOrDefault()
                                         let alreadyExists = originalUserDTO != null && AddOnDictionary(validate, i.Item.Email, $"{i.Item.Email} já cadastrado")
                                         let invalidEmail = i.Item.Email.InvalidEmail() && AddOnDictionary(validate, i.Item.Email, "E-mail inválido")
                                         let nonInformedName = i.Item.Name.InvalidLength(1, 150) && AddOnDictionary(validate, i.Item.Email, "Nome não informado")
                                         let nonInformedPassword = i.Item.Password.InvalidLength(1, 150) && AddOnDictionary(validate, i.Item.Email, "Senha não informada")
                                         let invalidPasswordMatch = i.Item.Password.InvalidStringMatch(i.Item.ConfirmPassword) && AddOnDictionary(validate, i.Item.Email, "Senhas não conferem")
                                         where !validate.ContainsKey(i.Item.Email)
                                         select new UserDTO().Create(new ExternalPropertiesUserDTO(i.Item.Name, EncryptService.Encrypt(i.Item.Password), i.Item.Email, EnumLanguage.Portuguese))).ToList();

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