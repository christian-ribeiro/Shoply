using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Base;
using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Translation.Interface.Service;
using System.Collections.Concurrent;
using System.Net.Mail;

namespace Shoply.Domain.Service;

public class BaseValidate<TValidateDTO, TProcessType>(ITranslationService translationService)
    where TValidateDTO : BaseValidateDTO
    where TProcessType : Enum
{
    #region Base
    internal virtual Task ValidateProcess(List<TValidateDTO> listValidateDTO, TProcessType processType) => throw new NotImplementedException();
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

    public static EnumValidateType InvalidBirthDate(DateOnly? birthDate, int minAge, bool required)
    {
        if (!birthDate.HasValue)
            return required ? EnumValidateType.NonInformed : EnumValidateType.Valid;

        if (birthDate > DateOnly.FromDateTime(DateTime.Today))
            return EnumValidateType.Invalid;

        DateTime today = DateTime.Today;
        int age = today.Year - birthDate.Value.Year;

        if (birthDate.Value > DateOnly.FromDateTime(today.AddYears(-age))) age--;

        if (age < minAge)
            return EnumValidateType.Invalid;

        return EnumValidateType.Valid;
    }
    #endregion

    #region Notification
    private class NotificationMessages
    {
        public const string InvalidRecordKey = "InvalidRecord";
        public const string InvalidEmailKey = "InvalidEmail";
        public const string EmailNotProvidedKey = "EmailNotProvided";
        public const string InvalidLengthKey = "InvalidLength";
        public const string ValueNotProvidedKey = "ValueNotProvided";
        public const string RecordsDoNotMatchKey = "RecordsDoNotMatch";
        public const string NoRecordsProvidedKey = "NoRecordsProvided";
        public const string AlreadyExistsKey = "AlreadyExists";
        public const string InvalidCPFKey = "InvalidCPF";
        public const string InvalidCNPJKey = "InvalidCNPJ";
        public const string CPFNonInformedKey = "CPFNonInformed";
        public const string CNPJNonInformedKey = "CNPJNonInformed";
        public const string InvalidBirthDateKey = "InvalidBirthDate";
    }

    private readonly ConcurrentDictionary<string, List<DetailedNotification>> validateMessages = [];

    public async Task<bool> ManualNotification(object key, string message, EnumValidateType enumValidateType)
    {
        return await Task.FromResult(HandleValidation(key?.ToString() ?? "", enumValidateType, message, string.Empty));
    }

    public async Task<bool> Invalid(int index)
    {
        return HandleValidation(index.ToString(), EnumValidateType.Invalid, await GetMessage(NotificationMessages.InvalidRecordKey), string.Empty);
    }

    public async Task<bool> AlreadyExists(string key)
    {
        return HandleValidation(key, EnumValidateType.Invalid, await GetMessage(NotificationMessages.AlreadyExistsKey, key), string.Empty);
    }

    public async Task<bool> InvalidEmail(int? index, string? email, EnumValidateType validateType)
    {
        string key = validateType == EnumValidateType.NonInformed ? index?.ToString()! : email!;
        return HandleValidation(key, validateType, await GetMessage(NotificationMessages.InvalidEmailKey, email), await GetMessage(NotificationMessages.EmailNotProvidedKey));
    }

    public async Task<bool> InvalidLength(string identifier, string? value, int minLength, int maxLength, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, await GetMessage(NotificationMessages.InvalidLengthKey, value, propertyName, minLength, maxLength), await GetMessage(NotificationMessages.ValueNotProvidedKey, propertyName));
    }

    public async Task<bool> InvalidMatch(string identifier, EnumValidateType validateType, params string[] propertiesName)
    {
        return HandleValidation(identifier, validateType, await GetMessage(NotificationMessages.RecordsDoNotMatchKey, string.Join(", ", propertiesName)), await GetMessage(NotificationMessages.NoRecordsProvidedKey));
    }

    public async Task<bool> InvalidCPF(string identifier, string? value, EnumValidateType validateType)
    {
        return HandleValidation(identifier, validateType, await GetMessage(NotificationMessages.InvalidCPFKey, value!), await GetMessage(NotificationMessages.CPFNonInformedKey));
    }

    public async Task<bool> InvalidCNPJ(string identifier, string? value, EnumValidateType validateType)
    {
        return HandleValidation(identifier, validateType, await GetMessage(NotificationMessages.InvalidCNPJKey, value!), await GetMessage(NotificationMessages.CNPJNonInformedKey));
    }

    public async Task<bool> InvalidBirthDate(string identifier, int minAge, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, await GetMessage(NotificationMessages.InvalidBirthDateKey, minAge), await GetMessage(NotificationMessages.ValueNotProvidedKey, propertyName));
    }
    #endregion

    #region Helpers
    private bool AddToDictionary(string key, DetailedNotification validationMessage)
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

    public async Task<bool> AddSuccessMessage(string key, string message)
    {
        return await Task.FromResult(AddToDictionary(key, new DetailedNotification(key, [message], EnumNotificationType.Success)));
    }

    private bool HandleValidation(string key, EnumValidateType validateType, string invalidMessage, string nonInformedMessage)
    {
        return validateType switch
        {
            EnumValidateType.Invalid => AddToDictionary(key, new DetailedNotification(key, [invalidMessage], EnumNotificationType.Error)),
            EnumValidateType.NonInformed => AddToDictionary(key, new DetailedNotification(key, [nonInformedMessage], EnumNotificationType.Error)),
            _ => true,
        };
    }

    public (List<DetailedNotification> Successes, List<DetailedNotification> Errors) GetValidationResults()
    {
        var successes = validateMessages.Values.SelectMany(v => v).Where(m => m.NotificationType == EnumNotificationType.Success).ToList();
        var errors = validateMessages.Values.SelectMany(v => v).Where(m => m.NotificationType != EnumNotificationType.Success).ToList();
        return (successes, errors);
    }
    #endregion

    private async Task<string> GetMessage(string key, params object[] args)
    {
        return await translationService.TranslateAsync(key, EnumLanguage.Portuguese, args);
    }
}