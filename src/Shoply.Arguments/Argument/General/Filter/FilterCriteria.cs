using Shoply.Arguments.Enum.General.Filter;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.General.Filter
{
    [method: JsonConstructor]
    public class FilterCriteria(string propertyName, EnumFilterOperator @operator, string? value, List<string>? listValue, string? minValue, string? maxValue)
    {
        public string PropertyName { get; private set; } = propertyName;
        public EnumFilterOperator Operator { get; private set; } = @operator;
        public string? Value { get; private set; } = value;
        public List<string>? ListValue { get; private set; } = listValue;
        public string? MinValue { get; private set; } = minValue;
        public string? MaxValue { get; private set; } = maxValue;
    }
}