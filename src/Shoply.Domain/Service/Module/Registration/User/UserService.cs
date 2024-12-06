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

                //_ = (from i in RemoveIgnore(listUserValidateDTO)
                //     where i.InputCreateUser == null
                //     let setIgnore = i.SetIgnore()
                //     select Invalid()).ToList();

                //_ = (from i in RemoveIgnore(listUserValidateDTO)
                //     let resultInvalidEmail = InvalidEmail(i.InputCreateUser?.Email)
                //     where resultInvalidEmail != EnumValidateType.Valid
                //     let setIgnore = resultInvalidEmail == EnumValidateType.NonInformed ? i.SetIgnore() : default
                //     let setInvalid = resultInvalidEmail == EnumValidateType.Invalid ? i.SetInvalid() : default
                //     select InvalidEmail(i.InputCreateUser?.Email, resultInvalidEmail)).ToList();

                //_ = (from i in RemoveIgnore(listUserValidateDTO)
                //     let resultInvalidLength = InvalidLength(i.InputCreateUser?.Name, 1, 150)
                //     where resultInvalidLength != EnumValidateType.Valid
                //     let setInvalid = i.SetInvalid()
                //     select InvalidLength(i.InputCreateUser?.Name, 1, 150, resultInvalidLength)).ToList();

                //_ = (from i in RemoveIgnore(listUserValidateDTO)
                //     let resultInvalidLength = InvalidLength(i.InputCreateUser?.Password, 6, 150)
                //     where resultInvalidLength != EnumValidateType.Valid
                //     let setInvalid = i.SetInvalid()
                //     select InvalidLength(i.InputCreateUser?.Password, 1, 150, resultInvalidLength)).ToList();

                //_ = (from i in RemoveIgnore(listUserValidateDTO)
                //     let resultInvalidMatch = InvalidMatch(i.InputCreateUser?.Password, i.InputCreateUser?.ConfirmPassword)
                //     where resultInvalidMatch != EnumValidateType.Valid
                //     let setInvalid = i.SetInvalid()
                //     select InvalidMatch(resultInvalidMatch)).ToList();

                foreach (var userValidateDTO in listUserValidateDTO)
                {
                    if (userValidateDTO.InputCreateUser == null)
                    {
                        userValidateDTO.SetIgnore();
                        Invalid(listUserValidateDTO.IndexOf(userValidateDTO));
                        continue;
                    }

                    var resultInvalidEmail = InvalidEmail(userValidateDTO.InputCreateUser.Email);
                    if (resultInvalidEmail != EnumValidateType.Valid)
                    {
                        if (resultInvalidEmail == EnumValidateType.NonInformed)
                            userValidateDTO.SetIgnore();
                        else
                            userValidateDTO.SetInvalid();
                        InvalidEmail(listUserValidateDTO.IndexOf(userValidateDTO), userValidateDTO.InputCreateUser.Email, resultInvalidEmail);
                    }

                    var resultNameInvalidLength = InvalidLength(userValidateDTO.InputCreateUser.Name, 1, 150);
                    if (resultNameInvalidLength != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidLength(userValidateDTO.InputCreateUser.Name, userValidateDTO.InputCreateUser.Email, userValidateDTO.InputCreateUser.Name, 1, 150, resultNameInvalidLength);
                    }

                    var resultPasswordInvalidLength = InvalidLength(userValidateDTO.InputCreateUser.Password, 6, 150);
                    if (resultPasswordInvalidLength != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidLength(userValidateDTO.InputCreateUser.Password, userValidateDTO.InputCreateUser.Email, userValidateDTO.InputCreateUser.Password, 6, 150, resultNameInvalidLength);
                    }

                    var resultPasswordInvalidMatch = InvalidMatch(userValidateDTO.InputCreateUser.Password, userValidateDTO.InputCreateUser.ConfirmPassword);
                    if (resultPasswordInvalidMatch != EnumValidateType.Valid)
                    {
                        userValidateDTO.SetInvalid();
                        InvalidMatch(userValidateDTO.InputCreateUser.Email, resultPasswordInvalidMatch);
                    }
                }

                break;
            case EnumProcessTypeGeneric.Update:
                break;
            case EnumProcessTypeGeneric.Delete:
                break;
        }
    }

    public override async Task<BaseResult<List<OutputUser?>>> Create(List<InputCreateUser> listInputCreateUser)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListIdentifier((from i in listInputCreateUser select new InputIdentifierUser(i.Email)).ToList());

        var listCreate = (from i in listInputCreateUser.Index()
                          select new
                          {
                              Index = i.Index,
                              InputCreateUser = i.Item,
                              ListRepeatedInputCreateUser = (from j in listInputCreateUser where listInputCreateUser.Count(x => x.Email == i.Item.Email) > 0 select j).ToList(),
                              OriginalUserDTO = (from j in listOriginalUserDTO where j.ExternalPropertiesDTO.Email == i.Item.Email select j).FirstOrDefault(),
                          }).ToList();

        List<UserValidateDTO> listUserValidateDTO = (from i in listCreate select new UserValidateDTO().ValidateCreate(i.InputCreateUser, i.ListRepeatedInputCreateUser, i.OriginalUserDTO)).ToList();
        ValidateProcess(listUserValidateDTO, EnumProcessTypeGeneric.Create);

        List<UserDTO> listCreateUserDTO = (from i in RemoveInvalid(listUserValidateDTO) select new UserDTO().Create(i.InputCreateUser!)).ToList();

        return BaseResult<List<OutputUser?>>.Success(FromDTOToOutput(await _repository.Create(listCreateUserDTO))!);
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
            return BaseResult<OutputAuthenticateUser>.Failure().AddError(new DetailedNotification(inputAuthenticateUser.Email, "Usuário ou senha inválidos"));

        string refreshToken = await jwtService.GenerateRefreshToken();

        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.RefreshToken), refreshToken);
        userDTO.InternalPropertiesDTO.SetProperty(nameof(userDTO.InternalPropertiesDTO.LoginKey), Guid.NewGuid());

        await _repository.Update(userDTO);

        string token = await jwtService.GenerateJwtToken(new JwtUser(userDTO.InternalPropertiesDTO.Id, userDTO.ExternalPropertiesDTO.Email, userDTO.ExternalPropertiesDTO.Name, userDTO.ExternalPropertiesDTO.Language));

        return BaseResult<OutputAuthenticateUser>.Success(new OutputAuthenticateUser(userDTO.InternalPropertiesDTO.Id, token));
    }
}