using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Base;
using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Domain.DTO.Base;
using System.Collections.Concurrent;
using System.Net.Mail;

namespace Shoply.Domain.Service;

public class BaseValidate<TValidateDTO, TProcessType>
    where TValidateDTO : BaseValidateDTO
    where TProcessType : Enum
{
    public BaseValidate()
    {
        validateMessages = [];
    }

    #region Base
    internal virtual void ValidateProcess(List<TValidateDTO> listValidateDTO, TProcessType processType) => throw new NotImplementedException();
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
    private class NotificationMessages
    {
        public const string InvalidRecord = "Registro inválido";
        public const string InvalidEmail = "O e-mail '{0}' informado não é válido";
        public const string EmailNotProvided = "E-mail não informado";
        public const string InvalidLength = "O valor {0} é inválido, '{1}' deve ser entre {2} e {3}";
        public const string ValueNotProvided = "Valor não informado para '{0}'";
        public const string RecordsDoNotMatch = "Os registros '{0}' não coincidem";
        public const string NoRecordsProvided = "Nenhum registro informado";
        public const string AlreadyExists = "O registro '{0}' já existe";
    }

    private static ConcurrentDictionary<string, List<DetailedNotification>> validateMessages = [];

    public static bool ManualNotification(string message, EnumValidateType enumValidateType)
    {
        return HandleValidation("", enumValidateType, message, string.Empty);
    }

    public static bool Invalid(int index)
    {
        return HandleValidation(index.ToString(), EnumValidateType.Invalid, NotificationMessages.InvalidRecord, string.Empty);
    }

    public static bool AlreadyExists(string key)
    {
        return HandleValidation(key, EnumValidateType.Invalid, string.Format(NotificationMessages.AlreadyExists, key), string.Empty);
    }

    public static bool InvalidEmail(int? index, string? email, EnumValidateType validateType)
    {
        string key = validateType == EnumValidateType.NonInformed ? index?.ToString()! : email!;
        return HandleValidation(key, validateType, string.Format(NotificationMessages.InvalidEmail, email), NotificationMessages.EmailNotProvided);
    }

    public static bool InvalidLength(string identifier, string? value, int minLength, int maxLength, EnumValidateType validateType, string? propertyName)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.InvalidLength, value, propertyName, minLength, maxLength), string.Format(NotificationMessages.ValueNotProvided, propertyName));
    }

    public static bool InvalidMatch(string identifier, EnumValidateType validateType, params string[] propertiesName)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.RecordsDoNotMatch, string.Join(", ", propertiesName)), NotificationMessages.NoRecordsProvided);
    }
    #endregion

    #region Helpers
    private static bool AddToDictionary(string key, DetailedNotification validationMessage)
    {
        var existingNotification = validateMessages.GetOrAdd(key, _ => [new(key, [], validationMessage.NotificationType)]);

        var notification = existingNotification.FirstOrDefault();
        if (notification != null)
        {
            notification.ListMessage ??= [];
            notification.ListMessage.AddRange(validationMessage.ListMessage ?? []);
        }

        return true;
    }

    public static bool AddSuccessMessage(string key, string message)
    {
        return AddToDictionary(key, new DetailedNotification(key, [message], EnumNotificationType.Success));
    }

    private static bool HandleValidation(string key, EnumValidateType validateType, string invalidMessage, string nonInformedMessage)
    {
        return validateType switch
        {
            EnumValidateType.Invalid => AddToDictionary(key, new DetailedNotification(key, [invalidMessage], EnumNotificationType.Error)),
            EnumValidateType.NonInformed => AddToDictionary(key, new DetailedNotification(key, [nonInformedMessage], EnumNotificationType.Error)),
            _ => true,
        };
    }

    public static (List<DetailedNotification> Successes, List<DetailedNotification> Errors) GetValidationResults()
    {
        var successes = validateMessages.Values.SelectMany(v => v).Where(m => m.NotificationType == EnumNotificationType.Success).ToList();
        var errors = validateMessages.Values.SelectMany(v => v).Where(m => m.NotificationType != EnumNotificationType.Success).ToList();
        return (successes, errors);
    }
    #endregion
}