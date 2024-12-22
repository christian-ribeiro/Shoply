using System.Runtime.Serialization;

namespace Shoply.CodeGenerator;

public enum EnumPropertyType
{
    [EnumMember(Value = "string")]
    String,
    [EnumMember(Value = "bool")]
    Bool,
    [EnumMember(Value = "long")]
    Long,
    [EnumMember(Value = "int")]
    Int,
    [EnumMember(Value = "decimal")]
    Decimal,
    [EnumMember(Value = "Guid")]
    Guid,
    [EnumMember(Value = "DateTime")]
    DateTime,
    [EnumMember(Value = "DateOnly")]
    DateOnly,
    [EnumMember(Value = "TimeOnly")]
    TimeOnly,
    [EnumMember(Value = "Enum")]
    Enum,
}