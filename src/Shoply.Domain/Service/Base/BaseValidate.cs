using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Base;
using System.Collections.Concurrent;
using System.Net.Mail;

namespace Shoply.Domain.Service;

public class BaseValidate<TValidateDTO, TProcessType>
    where TValidateDTO : BaseValidateDTO
    where TProcessType : Enum
{
    #region Base
    internal virtual void ValidateProcess(List<TValidateDTO> listValidateDTO, TProcessType processType) => throw new NotImplementedException();
    internal static List<TValidateDTO> RemoveIgnore(List<TValidateDTO> listValidateDTO) => (from i in listValidateDTO where !i.Ignore select i).ToList();
    internal static List<TValidateDTO> RemoveInvalid(List<TValidateDTO> listValidateDTO) => (from i in listValidateDTO where !i.Invalid select i).ToList();
    #endregion

    #region Validate
    public static EnumValidateType InvalidEmail(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return EnumValidateType.NonInformed;

        return MailAddress.TryCreate(email, out _) ? EnumValidateType.Valid : EnumValidateType.Invalid;
    }

    public static EnumValidateType InvalidLength(string? value, int minLength, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
            return minLength == 0 ? EnumValidateType.Valid : EnumValidateType.NonInformed;

        int length = value.Length;
        if (length < minLength || length > maxLength)
            return EnumValidateType.Invalid;

        return EnumValidateType.Valid;
    }

    public static EnumValidateType InvalidMatch(params string?[] value)
    {
        if (value == null || value.Length == 0)
            return EnumValidateType.NonInformed;

        string? firstValue = value[0];
        bool allMatch = Array.TrueForAll(value, v => v == firstValue);
        return allMatch ? EnumValidateType.Valid : EnumValidateType.Invalid;
    }
    #endregion

    #region Notification
    private static class NotificationMessages
    {
        public const string InvalidRecord = "Registro inválido";
        public const string InvalidEmail = "O e-mail '{0}' informado não é válido";
        public const string EmailNotProvided = "E-mail não informado";
        public const string InvalidLength = "O tamanho do campo '{0}' deve ser entre {1} e {2}";
        public const string ValueNotProvided = "Valor não informado para '{0}'";
        public const string RecordsDoNotMatch = "Os registros não coincidem";
        public const string NoRecordsProvided = "Nenhum registro informado";
    }

    internal static ConcurrentDictionary<string, List<string>> validate = [];

    public static bool Invalid(int index)
    {
        return HandleValidation(index.ToString(), EnumValidateType.Invalid, NotificationMessages.InvalidRecord, string.Empty);
    }

    public static bool InvalidEmail(int index, string? email, EnumValidateType validateType)
    {
        string key = email ?? index.ToString();
        return HandleValidation(key, validateType, string.Format(NotificationMessages.InvalidEmail, email), NotificationMessages.EmailNotProvided);
    }

    public static bool InvalidLength<TProperty>(TProperty property, string identifier, string? value, int minLength, int maxLength, EnumValidateType validateType)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.InvalidLength, typeof(TProperty).Name, minLength, maxLength), string.Format(NotificationMessages.ValueNotProvided, typeof(TProperty).Name));
    }

    public static bool InvalidMatch(string identifier, EnumValidateType validateType)
    {
        return HandleValidation(identifier, validateType, NotificationMessages.RecordsDoNotMatch, NotificationMessages.NoRecordsProvided);
    }
    #endregion

    #region Helpers
    private static bool AddOnDictionary(string key, string value)
    {
        validate.GetOrAdd(key, _ => new List<string>()).Add(value);
        return true;
    }

    private static bool HandleValidation(string key, EnumValidateType validateType, string invalidMessage, string nonInformedMessage)
    {
        return validateType switch
        {
            EnumValidateType.Invalid => AddOnDictionary(key, invalidMessage),
            EnumValidateType.NonInformed => AddOnDictionary(key, nonInformedMessage),
            _ => true,
        };
    }
    #endregion
}