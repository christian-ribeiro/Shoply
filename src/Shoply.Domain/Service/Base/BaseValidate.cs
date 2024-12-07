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

    public static EnumValidateType InvalidCPF(string cpf)
    {
        if (string.IsNullOrEmpty(cpf)) return EnumValidateType.NonInformed;

        cpf = new string(cpf.Where(char.IsDigit).ToArray());

        if (cpf.Length != 11 || cpf.Distinct().Count() == 1) return EnumValidateType.Invalid;

        int[] multiplier1 = [10, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [11, 10, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCpf = cpf[..9];
        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        int remainder = (sum * 10) % 11;
        if (remainder == 10 || remainder == 11) remainder = 0;

        string digit = remainder.ToString();
        tempCpf += digit;

        sum = 0;
        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        remainder = (sum * 10) % 11;
        if (remainder == 10 || remainder == 11) remainder = 0;

        digit += remainder.ToString();

        return cpf.EndsWith(digit) ? EnumValidateType.Valid : EnumValidateType.Invalid;
    }

    public static EnumValidateType InvalidCNPJ(string cnpj)
    {
        if (string.IsNullOrEmpty(cnpj)) return EnumValidateType.NonInformed;

        cnpj = new string(cnpj.Where(char.IsDigit).ToArray());

        if (cnpj.Length != 14 || cnpj.Distinct().Count() == 1) return EnumValidateType.Invalid;

        int[] multiplier1 = [5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];
        int[] multiplier2 = [6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2];

        string tempCnpj = cnpj[..12];
        int sum = 0;

        for (int i = 0; i < 12; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier1[i];

        int remainder = sum % 11;
        if (remainder < 2) remainder = 0;
        else remainder = 11 - remainder;

        string digit = remainder.ToString();
        tempCnpj += digit;

        sum = 0;
        for (int i = 0; i < 13; i++)
            sum += int.Parse(tempCnpj[i].ToString()) * multiplier2[i];

        remainder = sum % 11;
        if (remainder < 2) remainder = 0;
        else remainder = 11 - remainder;

        digit += remainder.ToString();

        return cnpj.EndsWith(digit) ? EnumValidateType.Valid : EnumValidateType.Invalid;
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

    public static EnumValidateType InvalidBirthDate(DateTime? birthDate, int minAge, bool required)
    {
        if (!birthDate.HasValue)
            return required ? EnumValidateType.NonInformed : EnumValidateType.Valid;

        if (birthDate > DateTime.Now)
            return EnumValidateType.Invalid;

        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Value.Year;

        if (birthDate.Value.Date > today.AddYears(-age)) age--;

        if (age < minAge)
            return EnumValidateType.Invalid;

        return EnumValidateType.Valid;
    }
    #endregion

    #region Notification
    private class NotificationMessages
    {
        public const string InvalidRecord = "Registro inválido";
        public const string InvalidEmail = "O e-mail '{0}' informado não é válido";
        public const string EmailNotProvided = "E-mail não informado";
        public const string InvalidLength = "O valor {0} é inválido, '{1}' deve ser entre '{2}' e '{3}'";
        public const string ValueNotProvided = "Valor não informado para '{0}'";
        public const string RecordsDoNotMatch = "Os registros '{0}' não coincidem";
        public const string NoRecordsProvided = "Nenhum registro informado";
        public const string AlreadyExists = "O registro '{0}' já existe";
        public const string InvalidCPF = "O CPF '{0}' informado é inválido";
        public const string InvalidCNPJ = "O CNPJ '{0}' informado é inválido";
        public const string CPFNonInformed = "O CPF não foi informado";
        public const string CNPJNonInformed = "O CNPJ não foi informado";
        public const string InvalidBirthDate = "Data de aniversário inválida. Deve possuir no mínimo '{0}' anos de idade";

    }

    private static ConcurrentDictionary<string, List<DetailedNotification>> validateMessages = [];

    public static bool ManualNotification(object key, string message, EnumValidateType enumValidateType)
    {
        return HandleValidation(key?.ToString() ?? "", enumValidateType, message, string.Empty);
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

    public static bool InvalidLength(string identifier, string? value, int minLength, int maxLength, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.InvalidLength, value, propertyName, minLength, maxLength), string.Format(NotificationMessages.ValueNotProvided, propertyName));
    }

    public static bool InvalidMatch(string identifier, EnumValidateType validateType, params string[] propertiesName)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.RecordsDoNotMatch, string.Join(", ", propertiesName)), NotificationMessages.NoRecordsProvided);
    }

    public static bool InvalidCPF(string identifier, string? value, EnumValidateType validateType)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.InvalidCPF, value), NotificationMessages.CPFNonInformed);
    }

    public static bool InvalidCNPJ(string identifier, string? value, EnumValidateType validateType)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.InvalidCNPJ, value), NotificationMessages.CNPJNonInformed);
    }

    public static bool InvalidBirthDate(string identifier, int minAge, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, string.Format(NotificationMessages.InvalidBirthDate, minAge), string.Format(NotificationMessages.ValueNotProvided, propertyName));
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