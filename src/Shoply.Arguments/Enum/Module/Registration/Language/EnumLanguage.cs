using System.Runtime.Serialization;

namespace Shoply.Arguments.Enum.Module.Registration;

public enum EnumLanguage
{
    [EnumMember(Value = "pt-br")]
    Portuguese,
    [EnumMember(Value = "en-us")]
    English,
    [EnumMember(Value = "es-es")]
    Spanish
}