using Shoply.Application.Argument.Authentication;
using Shoply.Application.Interface.Service.Authentication;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Security.Encryption;

namespace Shoply.Domain.Service.Module.Registration;

public class UserService(IUserRepository repository, IJwtService jwtService) : BaseService<IUserRepository, InputCreateUser, InputUpdateUser, InputIdentifierUser, OutputUser, InputIdentityUpdateUser, InputIdentityDeleteUser, UserValidateDTO, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO, EnumValidateProcessUser>(repository), IUserService
{
    internal override void ValidateProcess(List<UserValidateDTO> listUserValidateDTO, EnumValidateProcessUser processType)
    {
        switch (processType)
        {
            case EnumValidateProcessUser.Create:
                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputCreateUser == null)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var repeatedEmail = userValidateDTO.ListRepeatedInputCreateUser?.Count > 0;
                    if (repeatedEmail)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var resultInvalidEmail = InvalidEmail(userValidateDTO.InputCreateUser.Email);
                    if (resultInvalidEmail != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidEmail(listUserValidateDTO.IndexOf(userValidateDTO), userValidateDTO.InputCreateUser.Email, resultInvalidEmail);

                        if (resultInvalidEmail == EnumValidateType.NonInformed)
                            continue;
                    }

                    if (userValidateDTO.OriginalUserDTO != null)
                    {
                        userValidateDTO.SetInvalid();
                        AlreadyExists(userValidateDTO.InputCreateUser.Email);
                    }

                    var resultNameInvalidLength = InvalidLength(userValidateDTO.InputCreateUser.Name, 1, 150);
                    if (resultNameInvalidLength != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidLength(userValidateDTO.InputCreateUser.Email, userValidateDTO.InputCreateUser.Name, 1, 150, resultNameInvalidLength, nameof(userValidateDTO.InputCreateUser.Name));
                    }

                    var resultPasswordInvalidLength = InvalidLength(userValidateDTO.InputCreateUser.Password, 6, 150);
                    if (resultPasswordInvalidLength != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidLength(userValidateDTO.InputCreateUser.Email, userValidateDTO.InputCreateUser.Password, 6, 150, resultNameInvalidLength, nameof(userValidateDTO.InputCreateUser.Password));
                    }

                    var resultPasswordInvalidMatch = InvalidMatch(userValidateDTO.InputCreateUser.Password, userValidateDTO.InputCreateUser.ConfirmPassword);
                    if (resultPasswordInvalidMatch != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidMatch(userValidateDTO.InputCreateUser.Email, resultPasswordInvalidMatch, nameof(userValidateDTO.InputCreateUser.Password), nameof(userValidateDTO.InputCreateUser.ConfirmPassword));
                    }

                    if (!userValidateDTO.Invalid)
                        AddSuccessMessage(userValidateDTO.InputCreateUser.Email, "Usuário cadastrado com sucesso");
                }
                break;
            case EnumValidateProcessUser.Update:
                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputIdentityUpdateUser == null)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (userValidateDTO.InputIdentityUpdateUser.InputUpdate == null)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (userValidateDTO.OriginalUserDTO == null)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var repeatedInputUpdate = userValidateDTO.ListRepeatedInputIdentityUpdateUser?.Count > 0;
                    if (repeatedInputUpdate)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var resultNameInvalidLength = InvalidLength(userValidateDTO.InputIdentityUpdateUser.InputUpdate.Name, 1, 150);
                    if (resultNameInvalidLength != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidLength(userValidateDTO.OriginalUserDTO.ExternalPropertiesDTO.Email, userValidateDTO.InputIdentityUpdateUser.InputUpdate.Name, 1, 150, resultNameInvalidLength, nameof(userValidateDTO.InputCreateUser.Name));
                    }

                    if (!userValidateDTO.Invalid)
                        AddSuccessMessage(userValidateDTO.OriginalUserDTO.ExternalPropertiesDTO.Email, "Usuário alterado com sucesso");
                }
                break;
            case EnumValidateProcessUser.Delete:
                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputIdentityDeleteUser == null)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (userValidateDTO.OriginalUserDTO == null)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var repeatedInputDelete = userValidateDTO.ListRepeatedInputIdentityDeleteUser?.Count > 0;
                    if (repeatedInputDelete)
                    {
                        userValidateDTO.SetInvalid();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (!userValidateDTO.Invalid)
                        AddSuccessMessage(userValidateDTO.OriginalUserDTO.ExternalPropertiesDTO.Email, "Usuário excluído com sucesso");
                }
                break;

            case EnumValidateProcessUser.Authenticate:
                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputAuthenticateUser == null)
                    {
                        userValidateDTO.SetInvalid();
                        ManualNotification(listUserValidateDTO.IndexOf(userValidateDTO), "Usuário ou senha inválidos", EnumValidateType.Invalid);
                        continue;
                    }

                    if (userValidateDTO.OriginalUserDTO == null)
                    {
                        userValidateDTO.SetInvalid();
                        ManualNotification(listUserValidateDTO.IndexOf(userValidateDTO), "Usuário ou senha inválidos", EnumValidateType.Invalid);
                        continue;
                    }

                    if (string.IsNullOrEmpty(userValidateDTO.InputAuthenticateUser.Email))
                        userValidateDTO.SetInvalid();


                    if (string.IsNullOrEmpty(userValidateDTO.InputAuthenticateUser.Password))
                        userValidateDTO.SetInvalid();


                    if (userValidateDTO.InputAuthenticateUser.Password != null && !userValidateDTO.InputAuthenticateUser.Password.CompareHash(userValidateDTO.OriginalUserDTO.ExternalPropertiesDTO.Password))
                        userValidateDTO.SetInvalid();

                    if (userValidateDTO.Invalid)
                        ManualNotification(userValidateDTO.InputAuthenticateUser.Email, "Usuário ou senha inválidos", EnumValidateType.Invalid);
                }
                break;
        }
    }

    #region Create
    public override async Task<BaseResult<List<OutputUser?>>> Create(List<InputCreateUser> listInputCreateUser)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListIdentifier((from i in listInputCreateUser select new InputIdentifierUser(i.Email)).ToList());

        var listCreate = (from i in listInputCreateUser
                          select new
                          {
                              InputCreateUser = i,
                              ListRepeatedInputCreateUser = (from j in listInputCreateUser where listInputCreateUser.Count(x => x.Email == i.Email) > 1 select j).ToList(),
                              OriginalUserDTO = (from j in listOriginalUserDTO where j.ExternalPropertiesDTO.Email == i.Email select j).FirstOrDefault(),
                          }).ToList();

        List<UserValidateDTO> listUserValidateDTO = (from i in listCreate select new UserValidateDTO().ValidateCreate(i.InputCreateUser, i.ListRepeatedInputCreateUser, i.OriginalUserDTO)).ToList();
        ValidateProcess(listUserValidateDTO, EnumValidateProcessUser.Create);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateUser.Count)
            return BaseResult<List<OutputUser?>>.Failure(errors);

        List<UserDTO> listCreateUserDTO = (from i in RemoveInvalid(listUserValidateDTO) select new UserDTO().Create(i.InputCreateUser!.SetProperty(nameof(i.InputCreateUser.Password), EncryptService.Encrypt(i.InputCreateUser.Password)))).ToList();
        return BaseResult<List<OutputUser?>>.Success(FromDTOToOutput(await _repository.Create(listCreateUserDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputUser?>>> Update(List<InputIdentityUpdateUser> listInputIdentityUpdateUser)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId((from i in listInputIdentityUpdateUser select i.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateUser
                          select new
                          {
                              InputIdentityUpdateUser = i,
                              ListRepeatedInputIdentityUpdateUser = (from j in listInputIdentityUpdateUser where listInputIdentityUpdateUser.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalUserDTO = (from j in listOriginalUserDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<UserValidateDTO> listUserValidateDTO = (from i in listUpdate select new UserValidateDTO().ValidateUpdate(i.InputIdentityUpdateUser, i.ListRepeatedInputIdentityUpdateUser, i.OriginalUserDTO)).ToList();
        ValidateProcess(listUserValidateDTO, EnumValidateProcessUser.Update);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateUser.Count)
            return BaseResult<List<OutputUser?>>.Failure(errors);

        List<UserDTO> listUpdateUserDTO = (from i in RemoveInvalid(listUserValidateDTO) select i.OriginalUserDTO!.Update(i.InputIdentityUpdateUser!.InputUpdate!)).ToList();
        return BaseResult<List<OutputUser?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateUserDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteUser> listInputIdentityDeleteUser)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId((from i in listInputIdentityDeleteUser select i.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteUser
                          select new
                          {
                              InputIdentityDeleteUser = i,
                              ListRepeatedInputIdentityDeleteUser = (from j in listInputIdentityDeleteUser where listInputIdentityDeleteUser.Count(x => x.Id == i.Id) > 1 select j).ToList(),
                              OriginalUserDTO = (from j in listOriginalUserDTO where j.InternalPropertiesDTO.Id == i.Id select j).FirstOrDefault(),
                          }).ToList();

        List<UserValidateDTO> listUserValidateDTO = (from i in listDelete select new UserValidateDTO().ValidateDelete(i.InputIdentityDeleteUser, i.ListRepeatedInputIdentityDeleteUser, i.OriginalUserDTO)).ToList();
        ValidateProcess(listUserValidateDTO, EnumValidateProcessUser.Delete);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityDeleteUser.Count)
            return BaseResult<bool>.Failure(errors);

        List<UserDTO> listDeletepdateUserDTO = (from i in RemoveInvalid(listUserValidateDTO) select i.OriginalUserDTO).ToList();
        return BaseResult<bool>.Success(await _repository.Delete(listDeletepdateUserDTO), [.. successes, .. errors]);
    }
    #endregion

    #region Custom
    public async Task<BaseResult<OutputAuthenticateUser>> Authenticate(InputAuthenticateUser inputAuthenticateUser)
    {
        UserDTO? originalUserDTO = await _repository.GetByIdentifier(new InputIdentifierUser(inputAuthenticateUser.Email));

        UserValidateDTO userValidateDTO = new UserValidateDTO().ValidateAuthenticate(inputAuthenticateUser, originalUserDTO);
        ValidateProcess([userValidateDTO], EnumValidateProcessUser.Authenticate);

        var (_, errors) = GetValidationResults();
        if (errors.Count > 0)
            return BaseResult<OutputAuthenticateUser>.Failure(errors);

        string refreshToken = await jwtService.GenerateRefreshToken();

        originalUserDTO!.InternalPropertiesDTO.SetProperty(nameof(originalUserDTO.InternalPropertiesDTO.RefreshToken), refreshToken);
        originalUserDTO!.InternalPropertiesDTO.SetProperty(nameof(originalUserDTO.InternalPropertiesDTO.LoginKey), Guid.NewGuid());

        await _repository.Update(originalUserDTO);

        string token = await jwtService.GenerateJwtToken(new JwtUser(originalUserDTO.InternalPropertiesDTO.Id, originalUserDTO.ExternalPropertiesDTO.Email, originalUserDTO.ExternalPropertiesDTO.Name, originalUserDTO.InternalPropertiesDTO.LoginKey!.Value, originalUserDTO.ExternalPropertiesDTO.Language));

        return BaseResult<OutputAuthenticateUser>.Success(new OutputAuthenticateUser(originalUserDTO.InternalPropertiesDTO.Id, token));
    }
    #endregion
}