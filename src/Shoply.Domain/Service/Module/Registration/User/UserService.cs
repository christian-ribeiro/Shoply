using Shoply.Application.Argument.Authentication;
using Shoply.Application.Interface.Service.Authentication;
using Shoply.Application.Interface.Service.Integration;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Authenticate;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Arguments.Argument.Module.Registration;
using Shoply.Arguments.Extensions;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Repository.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Security.Encryption;
using Shoply.Translation.Interface.Service;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Shoply.Domain.Service.Module.Registration;

public class UserService(IUserRepository repository, ITranslationService translationService, IUserValidateService userValidateService, IJwtService jwtService, ISendEmailService sendEmailService) : BaseService<IUserRepository, IUserValidateService, InputCreateUser, InputUpdateUser, InputIdentityUpdateUser, InputIdentityDeleteUser, InputIdentifierUser, OutputUser, UserValidateDTO, UserDTO, InternalPropertiesUserDTO, ExternalPropertiesUserDTO, AuxiliaryPropertiesUserDTO>(repository, userValidateService, translationService), IUserService
{
    #region Create
    public override async Task<BaseResult<List<OutputUser?>>> Create(List<InputCreateUser> listInputCreateUser)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListIdentifier(listInputCreateUser.Select(x => new InputIdentifierUser(x.Email)).ToList());

        var listCreate = (from i in listInputCreateUser
                          select new
                          {
                              InputCreateUser = i,
                              ListRepeatedInputCreateUser = listInputCreateUser.GetDuplicateItem(i, x => new { x.Email }).ToList(),
                              OriginalUserDTO = listOriginalUserDTO.FirstOrDefault(x => x.ExternalPropertiesDTO.Email == i.Email),
                          }).ToList();

        List<UserValidateDTO> listUserValidateDTO = listCreate.Select(x => new UserValidateDTO().ValidateCreate(x.InputCreateUser, x.ListRepeatedInputCreateUser, x.OriginalUserDTO)).ToList();
        _validate.Create(listUserValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputCreateUser.Count)
            return BaseResult<List<OutputUser?>>.Failure(errors);

        List<UserDTO> listCreateUserDTO = (from i in RemoveInvalid(listUserValidateDTO) select new UserDTO().Create(i.InputCreate!.SetProperty(x => x.Password, EncryptService.Encrypt(i.InputCreate.Password)))).ToList();
        return BaseResult<List<OutputUser?>>.Success(FromDTOToOutput(await _repository.Create(listCreateUserDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Update
    public override async Task<BaseResult<List<OutputUser?>>> Update(List<InputIdentityUpdateUser> listInputIdentityUpdateUser)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId(listInputIdentityUpdateUser.Select(x => x.Id).ToList());

        var listUpdate = (from i in listInputIdentityUpdateUser
                          select new
                          {
                              InputIdentityUpdateUser = i,
                              ListRepeatedInputIdentityUpdateUser = listInputIdentityUpdateUser.GetDuplicateItem(i, x => new { x.Id }).ToList(),
                              OriginalUserDTO = listOriginalUserDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<UserValidateDTO> listUserValidateDTO = listUpdate.Select(x => new UserValidateDTO().ValidateUpdate(x.InputIdentityUpdateUser, x.ListRepeatedInputIdentityUpdateUser, x.OriginalUserDTO)).ToList();
        _validate.Update(listUserValidateDTO);

        var (successes, errors) = GetValidationResults();
        if (errors.Count == listInputIdentityUpdateUser.Count)
            return BaseResult<List<OutputUser?>>.Failure(errors);

        List<UserDTO> listUpdateUserDTO = (from i in RemoveInvalid(listUserValidateDTO) select i.OriginalUserDTO!.Update(i.InputIdentityUpdate!.InputUpdate!)).ToList();
        return BaseResult<List<OutputUser?>>.Success(FromDTOToOutput(await _repository.Update(listUpdateUserDTO))!, [.. successes, .. errors]);
    }
    #endregion

    #region Delete
    public override async Task<BaseResult<bool>> Delete(List<InputIdentityDeleteUser> listInputIdentityDeleteUser)
    {
        List<UserDTO> listOriginalUserDTO = await _repository.GetListByListId(listInputIdentityDeleteUser.Select(x => x.Id).ToList());

        var listDelete = (from i in listInputIdentityDeleteUser
                          select new
                          {
                              InputIdentityDeleteUser = i,
                              ListRepeatedInputIdentityDeleteUser = listInputIdentityDeleteUser.GetDuplicateItem(i, x => new { x.Id }),
                              OriginalUserDTO = listOriginalUserDTO.FirstOrDefault(x => x.InternalPropertiesDTO.Id == i.Id),
                          }).ToList();

        List<UserValidateDTO> listUserValidateDTO = listDelete.Select(x => new UserValidateDTO().ValidateDelete(x.InputIdentityDeleteUser, x.ListRepeatedInputIdentityDeleteUser, x.OriginalUserDTO)).ToList();
        _validate.Delete(listUserValidateDTO);

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
        _validate.Authenticate(userValidateDTO);

        var (_, errors) = GetValidationResults();
        if (userValidateDTO.Invalid)
            return BaseResult<OutputAuthenticateUser>.Failure(errors);

        string refreshToken = await jwtService.GenerateRefreshToken();

        originalUserDTO!.InternalPropertiesDTO
            .SetProperty(x => x.RefreshToken, refreshToken)
            .SetProperty(x => x.LoginKey, Guid.NewGuid());

        await _repository.Update(originalUserDTO);

        string token = await jwtService.GenerateJwtToken(new JwtUser(originalUserDTO.InternalPropertiesDTO.Id, originalUserDTO.ExternalPropertiesDTO.Email, originalUserDTO.ExternalPropertiesDTO.Name, originalUserDTO.InternalPropertiesDTO.LoginKey!.Value, originalUserDTO.ExternalPropertiesDTO.Language));

        return BaseResult<OutputAuthenticateUser>.Success(new OutputAuthenticateUser(originalUserDTO.InternalPropertiesDTO.Id, token));
    }

    public async Task<BaseResult<OutputAuthenticateUser>> RefreshToken(InputRefreshTokenUser inputRefreshTokenUser)
    {
        BaseResult<ClaimsPrincipal> principal = await jwtService.GetPrincipalFromExpiredToken(inputRefreshTokenUser.Token);
        _ = long.TryParse(principal?.Value?.FindFirst("user_id")?.Value, out long userId);

        UserDTO originalUserDTO = await _repository.Get(userId);

        UserValidateDTO userValidateDTO = new UserValidateDTO().ValidateRefreshToken(inputRefreshTokenUser, originalUserDTO);
        _validate.RefreshToken(userValidateDTO);

        var (_, errors) = GetValidationResults();
        if (userValidateDTO.Invalid)
            return BaseResult<OutputAuthenticateUser>.Failure(errors);

        string token = await jwtService.GenerateJwtToken(principal!.Value!.Claims.ToList());
        string refreshToken = await jwtService.GenerateRefreshToken();

        originalUserDTO.InternalPropertiesDTO
            .SetProperty(x => x.RefreshToken, refreshToken)
            .SetProperty(x => x.LoginKey, Guid.NewGuid());

        await _repository.Update(originalUserDTO);

        return BaseResult<OutputAuthenticateUser>.Success(new OutputAuthenticateUser(originalUserDTO.InternalPropertiesDTO.Id, token));
    }

    public async Task<BaseResult<bool>> SendEmailForgotPassword(InputSendEmailForgotPasswordUser inputSendEmailForgotPasswordUser)
    {
        UserDTO? originalUserDTO = await _repository.GetByIdentifier(new InputIdentifierUser(inputSendEmailForgotPasswordUser.Email));

        UserValidateDTO userValidateDTO = new UserValidateDTO().ValidateSendEmailForgotPassword(inputSendEmailForgotPasswordUser, originalUserDTO);
        _validate.SendEmailForgotPassword(userValidateDTO);

        var (_, errors) = GetValidationResults();
        if (userValidateDTO.Invalid)
            return BaseResult<bool>.Failure(errors);

        byte[] randomBytes = new byte[4];
        RandomNumberGenerator.Fill(randomBytes);
        string recoveryCode = (Math.Abs(BitConverter.ToInt32(randomBytes, 0)) % 1000000).ToString("D6");

        originalUserDTO!.InternalPropertiesDTO.SetProperty(x => x.PasswordRecoveryCode, recoveryCode);
        await _repository.Update(originalUserDTO);

        string htmlProto = File.ReadAllText("wwwroot/html-template/recovery-password.html");
        string userEncoded = WebUtility.HtmlEncode(originalUserDTO.ExternalPropertiesDTO.Name);

        htmlProto = htmlProto.Replace("{{USER}}", userEncoded);
        htmlProto = htmlProto.Replace("{{CODE}}", recoveryCode);

        var response = await sendEmailService.SendEmailAsync(inputSendEmailForgotPasswordUser.Email, "Esqueci a Senha", htmlProto, true, null);
        if (response)
            return BaseResult<bool>.Success(true);

        return BaseResult<bool>.Failure(errors);
    }

    public async Task<BaseResult<bool>> RedefinePasswordForgotPassword(InputRedefinePasswordForgotPasswordUser inputRedefinePasswordForgotPasswordUser)
    {
        UserDTO? originalUserDTO = await _repository.GetByFilter(x => x.PasswordRecoveryCode == inputRedefinePasswordForgotPasswordUser.PasswordRecoveryCode);
        UserValidateDTO userValidateDTO = new UserValidateDTO().ValidateRedefinePasswordForgotPassword(inputRedefinePasswordForgotPasswordUser, originalUserDTO);
        _validate.RedefinePasswordForgotPassword(userValidateDTO);

        var (_, errors) = GetValidationResults();
        if (userValidateDTO.Invalid)
            return BaseResult<bool>.Failure(errors);

        originalUserDTO!.ExternalPropertiesDTO.SetProperty(x => x.Password, EncryptService.Encrypt(inputRedefinePasswordForgotPasswordUser.NewPassword));
        originalUserDTO!.InternalPropertiesDTO.SetProperty(x => x.PasswordRecoveryCode, null);
        await _repository.Update(originalUserDTO);

        return BaseResult<bool>.Success(true);
    }

    public async Task<BaseResult<bool>> RedefinePassword(InputRedefinePasswordUser inputRedefinePasswordUser)
    {
        long loggedUserId = SessionData.GetLoggedUser(_guidSessionDataRequest)!.Id;

        UserDTO? originalUserDTO = await _repository.Get(loggedUserId);

        UserValidateDTO userValidateDTO = new UserValidateDTO().ValidateRedefinePassword(inputRedefinePasswordUser, originalUserDTO);
        _validate.RedefinePassword(userValidateDTO);

        var (_, errors) = GetValidationResults();
        if (userValidateDTO.Invalid)
            return BaseResult<bool>.Failure(errors);

        originalUserDTO.ExternalPropertiesDTO.SetProperty(x => x.Password, EncryptService.Encrypt(inputRedefinePasswordUser.NewPassword));
        await _repository.Update(originalUserDTO);

        return BaseResult<bool>.Success(true);
    }
    #endregion
}