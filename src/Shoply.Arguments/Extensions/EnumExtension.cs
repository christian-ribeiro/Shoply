using System.Reflection;
using System.Runtime.Serialization;

namespace Shoply.Arguments.Extensions;

public static class EnumExtension
{
    public static string GetMemberValue(this System.Enum value)
    {
        FieldInfo field = value.GetType().GetField(value.ToString())!;
        EnumMemberAttribute attribute = (EnumMemberAttribute)Attribute.GetCustomAttribute(field, typeof(EnumMemberAttribute))!;
        return attribute?.Value ?? System.Enum.GetName(value.GetType(), value)!;
    }
}