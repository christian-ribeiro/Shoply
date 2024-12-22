using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using System.Collections;
using System.Dynamic;

namespace Shoply.Api.Controller.Base;

public static class PrepareResponse
{
    public static BaseResult<object>? PrepareReturn<T>(Guid guidSessionDataRequest, BaseResult<T>? input)
    {
        if (input == null)
            return default;

        if (input.Value != null && input.IsSuccess)
        {
            var response = PrepareReturn(guidSessionDataRequest, input.Value!);
            return BaseResult<object>
                .Success(response!, input.ListNotification);
        }

        if (input.IsSuccess)
            return BaseResult<object>.Success(input.Value!, input.ListNotification);

        return BaseResult<object>.Failure(input.ListNotification);
    }

    public static object? PrepareReturn<T>(Guid guidSessionDataRequest, T input)
    {
        List<string>? listProperties = SessionData.GetReturnProperty(guidSessionDataRequest);
        if (input == null || listProperties == null || listProperties.Count == 0)
            return input;

        Type currentType = input.GetType();

        if (currentType.IsGenericType && typeof(List<>).IsAssignableFrom(currentType.GetGenericTypeDefinition()))
            return TransformObjectList((dynamic)input, listProperties);
        else
            return TransformObjectList([input], listProperties).FirstOrDefault();
    }

    public static List<dynamic> TransformObjectList<T>(List<T> listOriginalObject, List<string> properties)
    {
        return (from originalObject in listOriginalObject
                let returnPreparedProperties = PreparePropertyListReturn(originalObject, OrganizeProperties(properties))
                select returnPreparedProperties).ToList();
    }

    private static IDictionary<string, List<string>> OrganizeProperties(List<string> properties)
    {
        return (from returnProperty in properties
                let parts = returnProperty.Split('.')
                group parts by parts[0] into grouped
                select new
                {
                    Key = grouped.Key,
                    Values = (from parts in grouped
                              where parts.Length > 1
                              select string.Join(".", parts.Skip(1))).ToList()
                })
                .ToDictionary(item => item.Key, item => item.Values);
    }

    private static dynamic PreparePropertyListReturn<T>(T originalObject, IDictionary<string, List<string>> organizedProperty)
    {
        var expandoObj = new ExpandoObject();
        var expandoDict = (IDictionary<string, object>)expandoObj!;

        _ = (from item in from entry in organizedProperty
                          let propertyInfo = originalObject?.GetType().GetProperty(entry.Key)
                          where propertyInfo != null
                          let propertyValue = propertyInfo.GetValue(originalObject)
                          let preparedProperty = propertyValue != null
                                                  ? (entry.Value.Count == 0
                                                          ? propertyValue
                                                          : typeof(IEnumerable).IsAssignableFrom(propertyInfo.PropertyType) && propertyInfo.PropertyType != typeof(string)
                                                              ? (from i in ((IEnumerable)propertyValue).Cast<object>() select PreparePropertyListReturn(i, OrganizeProperties(entry.Value))).ToList()
                                                              : PreparePropertyListReturn(propertyValue, OrganizeProperties(entry.Value)))
                                                  : propertyValue is IEnumerable && propertyInfo.PropertyType != typeof(string)
                                                      ? new List<dynamic>()
                                                      : null
                          select new { entry.Key, PreparedProperty = preparedProperty }
             select expandoDict[item.Key] = item.PreparedProperty).ToList();

        return expandoObj;
    }
}