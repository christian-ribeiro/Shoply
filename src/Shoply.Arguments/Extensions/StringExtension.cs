namespace Shoply.Arguments.Extensions;

public static class StringExtension
{
    public static string GetOnlyDigit(this string value)
    {
        return new string([.. value.Where(char.IsDigit)]);
    }
}