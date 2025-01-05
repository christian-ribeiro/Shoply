using Shoply.Arguments.Enum.General.Filter;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.General.Filter
{
    [method: JsonConstructor]
    public class FilterItem(string propertyName, EnumFilterSearchType searchType, string? value, List<string>? listValue, string? minValue, string? maxValue, EnumFilterCondition? filterCondition, List<FilterItem> listFilterItem)
    {
        public string PropertyName { get; private set; } = propertyName;
        public EnumFilterSearchType SearchType { get; private set; } = searchType;
        public string? Value { get; private set; } = value;
        public List<string>? ListValue { get; private set; } = listValue;
        public string? MinValue { get; private set; } = minValue;
        public string? MaxValue { get; private set; } = maxValue;
        public EnumFilterCondition? FilterCondition { get; private set; } = filterCondition;
        public List<FilterItem> ListFilterItem { get; set; } = listFilterItem;
    }
}