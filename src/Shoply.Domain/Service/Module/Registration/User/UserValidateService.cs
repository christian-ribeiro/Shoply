using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Module.Registration;
using Shoply.Domain.Interface.Service.Module.Registration;
using Shoply.Domain.Service.Base;
using Shoply.Security.Encryption;
using Shoply.Translation.Interface.Service;

namespace Shoply.Domain.Service.Module.Registration;

public class UserValidateService(ITranslationService translationService) : BaseValidateService<UserValidateDTO>(translationService), IUserValidateService
{
    public void Create(List<UserValidateDTO> listUserValidateDTO)
    {
        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.InputCreate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.ListRepeatedInputCreate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             let resultInvalidEmail = InvalidEmail(i.InputCreate!.Email)
             where resultInvalidEmail != EnumValidateType.Valid
             let setInvalid = resultInvalidEmail == EnumValidateType.Invalid ? i.SetInvalid() : i.SetIgnore()
             select InvalidGeneric(listUserValidateDTO.IndexOf(i), i.InputCreate?.Email, nameof(i.InputCreate.Email), resultInvalidEmail)).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.OriginalUserDTO != null
             let setInvalid = i.SetInvalid()
             select AlreadyExists(i.InputCreate!.Email)).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Name, 1, 150)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Email, i.InputCreate.Name, 1, 150, resultInvalidLength, nameof(i.InputCreate.Name))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputCreate!.Password, 6, 150)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.InputCreate!.Email, i.InputCreate.Password, 6, 150, resultInvalidLength, nameof(i.InputCreate.Password))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             let resultInvalidMatch = InvalidMatch(i.InputCreate!.Password, i.InputCreate!.ConfirmPassword)
             where resultInvalidMatch != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidMatch(i.InputCreate!.Email, resultInvalidMatch, nameof(i.InputCreate.Password), nameof(i.InputCreate.ConfirmPassword))).ToList();

        _ = (from i in RemoveInvalid(listUserValidateDTO)
             select AddSuccessMessage(i.InputCreate!.Email, GetMessage(NotificationMessages.SuccessfullyRegisteredKey, "Usuário"))).ToList();
    }

    public void Update(List<UserValidateDTO> listUserValidateDTO)
    {
        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.InputIdentityUpdate?.InputUpdate == null
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.OriginalUserDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.ListRepeatedInputIdentityUpdate?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             let resultInvalidLength = InvalidLength(i.InputIdentityUpdate!.InputUpdate!.Name, 1, 150)
             where resultInvalidLength != EnumValidateType.Valid
             let setInvalid = i.SetInvalid()
             select InvalidLength(i.OriginalUserDTO!.ExternalPropertiesDTO.Email, i.InputIdentityUpdate!.InputUpdate!.Name, 1, 150, resultInvalidLength, nameof(i.InputIdentityUpdate.InputUpdate.Name))).ToList();

        _ = (from i in RemoveInvalid(listUserValidateDTO)
             select AddSuccessMessage(i.OriginalUserDTO!.ExternalPropertiesDTO.Email, GetMessage(NotificationMessages.SuccessfullyUpdatedKey, "Usuário"))).ToList();
    }

    public void Delete(List<UserValidateDTO> listUserValidateDTO)
    {
        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.ListRepeatedInputIdentityDelete == null
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.OriginalUserDTO == null
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveIgnore(listUserValidateDTO)
             where i.ListRepeatedInputIdentityDelete?.Count > 0
             let setIgnore = i.SetIgnore()
             select Invalid(listUserValidateDTO.IndexOf(i))).ToList();

        _ = (from i in RemoveInvalid(listUserValidateDTO)
             select AddSuccessMessage(i.OriginalUserDTO!.ExternalPropertiesDTO.Email, GetMessage(NotificationMessages.SuccessfullyDeletedKey, "Usuário"))).ToList();
    }

    public void Authenticate(UserValidateDTO userValidateDTO)
    {
        if (!userValidateDTO.Ignore && userValidateDTO.InputAuthenticate == null)
        {
            userValidateDTO.SetIgnore();
        }

        if (!userValidateDTO.Ignore && userValidateDTO.OriginalUserDTO == null)
        {
            userValidateDTO.SetIgnore();
        }

        if (!userValidateDTO.Ignore && string.IsNullOrEmpty(userValidateDTO.InputAuthenticate!.Email))
        {
            userValidateDTO.SetInvalid();
        }

        if (!userValidateDTO.Ignore && string.IsNullOrEmpty(userValidateDTO.InputAuthenticate!.Password))
        {
            userValidateDTO.SetInvalid();
        }

        if (!userValidateDTO.Ignore && !string.IsNullOrEmpty(userValidateDTO.InputAuthenticate!.Password) && !userValidateDTO.InputAuthenticate!.Password.CompareHash(userValidateDTO.OriginalUserDTO!.ExternalPropertiesDTO.Password))
        {
            userValidateDTO.SetInvalid();
        }

        if (userValidateDTO.Invalid)
            ManualNotification(userValidateDTO.InputAuthenticate!.Email, GetMessage(NotificationMessages.InvalidUserPasswordKey), EnumValidateType.Invalid);
    }

    public void RefreshToken(UserValidateDTO userValidateDTO)
    {
        if (!userValidateDTO.Ignore && userValidateDTO.InputRefreshToken == null)
        {
            userValidateDTO.SetIgnore();
            ManualNotification(0, GetMessage(NotificationMessages.InvalidUserPasswordKey), EnumValidateType.Invalid);
        }

        if (!userValidateDTO.Ignore && userValidateDTO.OriginalUserDTO == null)
        {
            userValidateDTO.SetIgnore();
            ManualNotification(0, GetMessage(NotificationMessages.InvalidUserPasswordKey), EnumValidateType.Invalid);
        }

        if (!userValidateDTO.Ignore && string.IsNullOrEmpty(userValidateDTO.InputRefreshToken!.RefreshToken))
            userValidateDTO.SetInvalid();

        if (!userValidateDTO.Ignore && userValidateDTO.InputRefreshToken!.RefreshToken != userValidateDTO.OriginalUserDTO!.InternalPropertiesDTO.RefreshToken)
            userValidateDTO.SetInvalid();

        if (!userValidateDTO.Ignore && userValidateDTO.Invalid)
            ManualNotification(userValidateDTO.InputAuthenticate?.Email ?? "", GetMessage(NotificationMessages.InvalidUserPasswordKey), EnumValidateType.Invalid);
    }

    public void SendEmailForgotPassword(UserValidateDTO userValidateDTO)
    {
        if (!userValidateDTO.Ignore && userValidateDTO.InputSendEmailForgotPassword == null)
        {
            userValidateDTO.SetIgnore();
            Invalid(0);
        }

        if (!userValidateDTO.Ignore && userValidateDTO.OriginalUserDTO == null)
        {
            userValidateDTO.SetIgnore();
            Invalid(0);
        }

        if (!userValidateDTO.Ignore)
        {
            var resultInvalidEmail = InvalidEmail(userValidateDTO.InputSendEmailForgotPassword!.Email);

            if (resultInvalidEmail != EnumValidateType.Valid)
            {
                if (resultInvalidEmail == EnumValidateType.Invalid)
                    userValidateDTO.SetInvalid();

                if (resultInvalidEmail == EnumValidateType.NonInformed)
                    userValidateDTO.SetIgnore();

                InvalidGeneric(0, userValidateDTO.InputSendEmailForgotPassword!.Email, nameof(userValidateDTO.InputSendEmailForgotPassword.Email), resultInvalidEmail);
            }
        }
    }

    public void RedefinePasswordForgotPassword(UserValidateDTO userValidateDTO)
    {
        if (!userValidateDTO.Ignore && userValidateDTO.InputRedefinePasswordForgotPassword == null)
        {
            userValidateDTO.SetIgnore();
            Invalid(0);
        }

        if (!userValidateDTO.Ignore && userValidateDTO.OriginalUserDTO == null)
        {
            userValidateDTO.SetIgnore();
            Invalid(0);
        }

        if (!userValidateDTO.Ignore)
        {
            var resultInvalidMatch = InvalidMatch(userValidateDTO.InputRedefinePasswordForgotPassword?.NewPassword, userValidateDTO.InputRedefinePasswordForgotPassword?.ConfirmNewPassword);
            if (resultInvalidMatch != EnumValidateType.Valid)
            {
                userValidateDTO.SetInvalid();
                InvalidMatch(userValidateDTO.OriginalUserDTO!.ExternalPropertiesDTO.Email, resultInvalidMatch, nameof(userValidateDTO.InputRedefinePasswordForgotPassword.NewPassword), nameof(userValidateDTO.InputRedefinePasswordForgotPassword.ConfirmNewPassword));
            }
        }
    }

    public void RedefinePassword(UserValidateDTO userValidateDTO)
    {
        if (!userValidateDTO.Ignore && userValidateDTO.InputRedefinePassword == null)
        {
            userValidateDTO.SetIgnore();
            Invalid(0);
        }

        if (!userValidateDTO.Ignore && userValidateDTO.OriginalUserDTO == null)
        {
            userValidateDTO.SetIgnore();
            Invalid(0);
        }

        if (!userValidateDTO.Ignore && string.IsNullOrEmpty(userValidateDTO.InputRedefinePassword!.CurrentPassword))
        {
            userValidateDTO.SetInvalid();
            InvalidGeneric(userValidateDTO.OriginalUserDTO!.ExternalPropertiesDTO.Email, userValidateDTO.InputRedefinePassword.CurrentPassword, nameof(userValidateDTO.InputRedefinePassword.CurrentPassword), EnumValidateType.NonInformed); ;
        }

        if (!userValidateDTO.Ignore && !string.IsNullOrEmpty(userValidateDTO.InputRedefinePassword!.CurrentPassword) && !userValidateDTO.InputRedefinePassword!.CurrentPassword.CompareHash(userValidateDTO.OriginalUserDTO!.ExternalPropertiesDTO.Password))
        {
            userValidateDTO.SetInvalid();
            InvalidGeneric(userValidateDTO.OriginalUserDTO!.ExternalPropertiesDTO.Email, userValidateDTO.InputRedefinePassword.CurrentPassword, nameof(userValidateDTO.InputRedefinePassword.CurrentPassword), EnumValidateType.Invalid); ;
        }

        if (!userValidateDTO.Ignore)
        {
            var resultInvalidMatch = InvalidMatch(userValidateDTO.InputRedefinePassword?.NewPassword, userValidateDTO.InputRedefinePassword?.ConfirmNewPassword);
            if (resultInvalidMatch != EnumValidateType.Invalid)
            {
                userValidateDTO.SetInvalid();
                InvalidMatch(userValidateDTO.OriginalUserDTO!.ExternalPropertiesDTO.Email, resultInvalidMatch, nameof(userValidateDTO.InputRedefinePassword.NewPassword), nameof(userValidateDTO.InputRedefinePassword.ConfirmNewPassword));
            }
        }
    }
}