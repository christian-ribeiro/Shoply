using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Enum.Base;
using Shoply.Arguments.Enum.Base.Validate;
using Shoply.Arguments.Enum.Module.Registration;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Utils;
using Shoply.Translation.Interface.Service;
using System.Net.Mail;

namespace Shoply.Domain.Service;

public class BaseValidate<TValidateDTO>(ITranslationService translationService)
    where TValidateDTO : BaseValidateDTO
{
    public Guid _guidSessionDataRequest;

    #region Base
    internal static List<TValidateDTO> RemoveInvalid(List<TValidateDTO> listValidateDTO) => [.. (from i in listValidateDTO where !i.Invalid select i)];
    internal static List<TValidateDTO> RemoveIgnore(List<TValidateDTO> listValidateDTO) => [.. (from i in listValidateDTO where !i.Ignore select i)];
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

    public EnumValidateType InvalidRelatedProperty(object? value, object? relatedProperty)
    {
        if (value == null)
            return EnumValidateType.Valid;

        return value.Equals(0) ? EnumValidateType.NonInformed : !value.Equals(0) && relatedProperty == null ? EnumValidateType.Invalid : EnumValidateType.Valid;
    }
    #endregion

    #region Notification
    internal static class NotificationMessages
    {
        public const string InvalidLengthKey = "InvalidLength";
        public const string RecordsDoNotMatchKey = "RecordsDoNotMatch";
        public const string AlreadyExistsKey = "AlreadyExists";
        public const string InvalidBirthDateKey = "InvalidBirthDate";
        public const string SuccessfullyRegisteredKey = "SuccessfullyRegistered";
        public const string SuccessfullyUpdatedKey = "SuccessfullyUpdated";
        public const string SuccessfullyDeletedKey = "SuccessfullyDeleted";
        public const string InvalidUserPasswordKey = "InvalidUserPassword";
        public const string InvalidRecordKey = "InvalidRecord";
        public const string GenericInvalidValueKey = "GenericInvalidValue";
        public const string GenericNotProvideKey = "GenericNotProvide";
    }

    public bool ManualNotification(object key, string message, EnumValidateType enumValidateType)
    {
        return HandleValidation(key?.ToString() ?? "", enumValidateType, message, string.Empty);
    }

    public bool Invalid(int index)
    {
        return HandleValidation(index.ToString(), EnumValidateType.Invalid, GetMessage(NotificationMessages.InvalidRecordKey), string.Empty);
    }

    public bool AlreadyExists(string key)
    {
        return HandleValidation(key, EnumValidateType.Invalid, GetMessage(NotificationMessages.AlreadyExistsKey, key), string.Empty);
    }

    public bool InvalidGeneric(int? index, object? value, string propertyName, EnumValidateType validateType)
    {
        string key = validateType == EnumValidateType.NonInformed ? index?.ToString()! : value?.ToString() ?? "";
        return HandleValidation(key, validateType, GetMessage(NotificationMessages.GenericInvalidValueKey, propertyName, value!), GetMessage(NotificationMessages.GenericNotProvideKey, propertyName));
    }

    public bool InvalidGeneric(string identifier, object? value, string propertyName, EnumValidateType validateType)
    {
        var teste1 = GetMessage(NotificationMessages.GenericInvalidValueKey, propertyName, value!);
        var teste2 = GetMessage(NotificationMessages.GenericNotProvideKey, propertyName);
        return HandleValidation(identifier, validateType, teste1, teste2);
    }

    public bool InvalidLength(string identifier, string? value, int minLength, int maxLength, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, GetMessage(NotificationMessages.InvalidLengthKey, value!, propertyName, minLength, maxLength), GetMessage(NotificationMessages.GenericNotProvideKey, propertyName));
    }

    public bool InvalidMatch(string identifier, EnumValidateType validateType, params string[] propertiesName)
    {
        return HandleValidation(identifier, validateType, GetMessage(NotificationMessages.RecordsDoNotMatchKey, string.Join(", ", propertiesName)), GetMessage(NotificationMessages.GenericNotProvideKey));
    }

    public bool InvalidBirthDate(string identifier, int minAge, EnumValidateType validateType, string propertyName)
    {
        return HandleValidation(identifier, validateType, GetMessage(NotificationMessages.InvalidBirthDateKey, minAge), GetMessage(NotificationMessages.GenericNotProvideKey, propertyName));
    }

    public bool InvalidRelatedProperty(string identifier, object? value, string propertyName, EnumValidateType enumValidateType)
    {
        return HandleValidation(identifier, enumValidateType, GetMessage(NotificationMessages.GenericInvalidValueKey, propertyName, value!), GetMessage(NotificationMessages.GenericNotProvideKey, propertyName));
    }
    #endregion

    #region Helpers
    private bool AddToDictionary(string key, DetailedNotification validationMessage)
    {
        NotificationHelper.Add(_guidSessionDataRequest, key, validationMessage);
        return true;
    }

    public bool AddSuccessMessage(string key, string message)
    {
        return AddToDictionary(key, new DetailedNotification(key, [message], EnumNotificationType.Success));
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
        var validateMessages = NotificationHelper.Get(_guidSessionDataRequest);

        var successes = validateMessages.Where(m => m.NotificationType == EnumNotificationType.Success).ToList();
        var errors = validateMessages.Where(m => m.NotificationType != EnumNotificationType.Success).ToList();
        return (successes, errors);
    }
    #endregion

    internal string GetMessage(string key, params object[] args)
    {
        return translationService.Translate(key, EnumLanguage.Portuguese, args);
    }

    #region Internal
    public void SetGuid(Guid guidSessionDataRequest)
    {
        _guidSessionDataRequest = guidSessionDataRequest;
    }
    #endregion
}