using Shoply.Application.Argument.Authentication;
using Shoply.Application.Interface.Service.Authentication;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Enum.Base;
using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Security.Encryption;

namespace Shoply.Domain.Service.Module.Registration;

public class UserService(IUserRepository repository, IJwtService jwtService) : BaseService<IUserRepository, InputCreateUser, InputUpdateUser, InputIdentifierUser, OutputUser, InputIdentityUpdateUser, InputIdentityDeleteUser, UserValidateDTO, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO, EnumProcessTypeGeneric>(repository), IUserService
{
    internal override void ValidateProcess(List<UserValidateDTO> listUserValidateDTO, EnumProcessTypeGeneric processType)
    {
        switch (processType)
        {
            case EnumProcessTypeGeneric.Create:

                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputCreateUser == null)
                    {
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var repeatedEmail = userValidateDTO.ListRepeatedInputCreateUser?.Count > 0;
                    if (repeatedEmail)
                    {
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
            case EnumProcessTypeGeneric.Update:

                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputIdentityUpdateUser == null)
                    {
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (userValidateDTO.InputIdentityUpdateUser.InputUpdate == null)
                    {
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (userValidateDTO.OriginalUserDTO == null)
                    {
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var repeatedInputUpdate = userValidateDTO.ListRepeatedInputIdentityUpdateUser?.Count > 0;
                    if (repeatedInputUpdate)
                    {
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
            case EnumProcessTypeGeneric.Delete:

                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputIdentityDeleteUser == null)
                    {
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (userValidateDTO.OriginalUserDTO == null)
                    {
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var repeatedInputDelete = userValidateDTO.ListRepeatedInputIdentityDeleteUser?.Count > 0;
                    if (repeatedInputDelete)
                    {
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    if (!userValidateDTO.Invalid)
                        AddSuccessMessage(userValidateDTO.OriginalUserDTO.ExternalPropertiesDTO.Email, "Usuário excluído com sucesso");
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
        ValidateProcess(listUserValidateDTO, EnumProcessTypeGeneric.Create);

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
        ValidateProcess(listUserValidateDTO, EnumProcessTypeGeneric.Update);

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
        ValidateProcess(listUserValidateDTO, EnumProcessTypeGeneric.Delete);

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
        UserDTO? userDTO = await _repository.GetByIdentifier(new InputIdentifierUser(inputAuthenticateUser.Email));

        if (userDTO == null || !inputAuthenticateUser.Password.CompareHash(userDTO.ExternalPropertiesDTO.Password))
            return BaseResult<OutputAuthenticateUser>.Failure(new DetailedNotification(inputAuthenticateUser.Email, ["Usuário ou senha inválidos"], EnumNotificationType.Error));

        string refreshToken = await jwtService.GenerateRefreshToken();

        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.RefreshToken), refreshToken);
        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.LoginKey), Guid.NewGuid());

        await _repository.Update(userDTO);

        string token = await jwtService.GenerateJwtToken(new JwtUser(userDTO.InternalPropertiesDTO.Id, userDTO.ExternalPropertiesDTO.Email, userDTO.ExternalPropertiesDTO.Name, userDTO.ExternalPropertiesDTO.Language));

        return BaseResult<OutputAuthenticateUser>.Success(new OutputAuthenticateUser(userDTO.InternalPropertiesDTO.Id, token));
    }
    #endregion
}