using System.Text.RegularExpressions;

namespace Shoply.Arguments.Validate;

public static class BaseValidate
{
    public static bool InvalidEmail(this string email)
    {
        if (string.IsNullOrEmpty(email))
            return false;

        var emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        var regex = new Regex(emailPattern);

        return !regex.IsMatch(email);
    }

    public static bool InvalidLength(this string? value, int minLength, int maxLength)
    {
        if (value?.Length < minLength || value?.Length > maxLength)
            return true;

        return false;
    }

    public static bool InvalidStringMatch(this string? value, string? secondValue)
    {
        return value != secondValue;
    }
}