using Shoply.Arguments.Enum.General.Filter;
using System.Text.Json.Serialization;

namespace Shoply.Arguments.Argument.General.Filter;

[method: JsonConstructor]
public class InputFilter(EnumFilterCondition filterCondition, List<FilterItem> listFilterItem)
{
    public EnumFilterCondition FilterCondition { get; set; } = filterCondition;
    public List<FilterItem> ListFilterItem { get; set; } = listFilterItem;
}