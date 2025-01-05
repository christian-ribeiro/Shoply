using Shoply.Arguments.Argument.General.Filter;
using Shoply.Arguments.Enum.General.Filter;
using System.Collections;
using System.Linq.Expressions;

namespace Shoply.Domain.Utils;

public static class FilterBuilderHelper
{
    public static Expression<Func<TOutput, bool>> BuildPredicate<TOutput>(InputFilter inputFilter)
    {
        var parameter = Expression.Parameter(typeof(TOutput), "x");
        Expression? body = null;

        foreach (var filter in inputFilter.ListFilterItem)
        {
            var filterExpression = BuildFilterExpression<TOutput>(parameter, filter, inputFilter.FilterCondition);

            if (filterExpression != null)
                body = body == null ? filterExpression : ApplyFilterCondition(body, filterExpression, inputFilter.FilterCondition);
        }

        return Expression.Lambda<Func<TOutput, bool>>(body ?? Expression.Constant(true), parameter);
    }

    public static Expression<Func<TOutput, bool>> BuildPredicate<TOutput>(InputFilter inputFilter, EnumFilterCondition filterCondition)
    {
        var parameter = Expression.Parameter(typeof(TOutput), "x");
        Expression? body = null;

        foreach (var filter in inputFilter.ListFilterItem)
        {
            var filterExpression = BuildFilterExpression<TOutput>(parameter, filter, filterCondition);

            if (filterExpression != null)
                body = body = body == null ? filterExpression : ApplyFilterCondition(body, filterExpression, filterCondition);
        }

        return Expression.Lambda<Func<TOutput, bool>>(body ?? Expression.Constant(true), parameter);
    }

    private static Expression? BuildFilterExpression<TOutput>(ParameterExpression parameter, FilterItem filter, EnumFilterCondition filterCondition)
    {
        var property = Expression.Property(parameter, filter.PropertyName);
        var propertyType = property.Type;

        if (!IsValidForFilterType(propertyType, filter))
            return null;

        Expression? comparison = null;

        if (propertyType.IsEnum)
        {
            var convertedProperty = Expression.Convert(property, typeof(int));
            propertyType = typeof(int);

            comparison = filter.SearchType switch
            {
                EnumFilterSearchType.Equals => Expression.Equal(convertedProperty, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.NotEquals => Expression.NotEqual(convertedProperty, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.GreaterThan => Expression.GreaterThan(convertedProperty, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.LessThan => Expression.LessThan(convertedProperty, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.GreaterThanOrEqual => Expression.GreaterThanOrEqual(convertedProperty, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.LessThanOrEqual => Expression.LessThanOrEqual(convertedProperty, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.IsNull => Expression.Equal(convertedProperty, Expression.Constant(null)),
                EnumFilterSearchType.IsNotNull => Expression.NotEqual(convertedProperty, Expression.Constant(null)),
                EnumFilterSearchType.Between => HandleBetween(property, filter),
                EnumFilterSearchType.ListContains => HandleListContains(property, filter),
                EnumFilterSearchType.ListNotContains => HandleListNotContains(property, filter),
                _ => null
            };
        }
        else
        {
            comparison = filter.SearchType switch
            {
                EnumFilterSearchType.Equals => Expression.Equal(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.NotEquals => Expression.NotEqual(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.GreaterThan => Expression.GreaterThan(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.LessThan => Expression.LessThan(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.LessThanOrEqual => Expression.LessThanOrEqual(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterSearchType.Contains => Expression.Call(property, typeof(string).GetMethod("Contains", [typeof(string)])!, Expression.Constant(filter.Value)),
                EnumFilterSearchType.StartsWith => Expression.Call(property, typeof(string).GetMethod("StartsWith", [typeof(string)])!, Expression.Constant(filter.Value)),
                EnumFilterSearchType.EndsWith => Expression.Call(property, typeof(string).GetMethod("EndsWith", [typeof(string)])!, Expression.Constant(filter.Value)),
                EnumFilterSearchType.IsNull => Expression.Equal(property, Expression.Constant(null)),
                EnumFilterSearchType.IsNotNull => Expression.NotEqual(property, Expression.Constant(null)),
                EnumFilterSearchType.Between => HandleBetween(property, filter),
                EnumFilterSearchType.ListContains => HandleListContains(property, filter),
                EnumFilterSearchType.ListNotContains => HandleListNotContains(property, filter),
                _ => null
            };
        }

        if (filter.ListFilterItem?.Count > 0)
        {
            var subFilterExpression = BuildPredicate<TOutput>(new InputFilter(filter.FilterCondition ?? EnumFilterCondition.And, filter.ListFilterItem), filter.FilterCondition ?? EnumFilterCondition.And);
            comparison = ApplyFilterCondition(comparison, subFilterExpression.Body, filterCondition);
        }

        return comparison;
    }

    private static Expression ApplyFilterCondition(Expression? currentComparison, Expression subFilterExpression, EnumFilterCondition? filterCondition)
    {
        if (currentComparison == null)
            return subFilterExpression;

        return filterCondition switch
        {
            EnumFilterCondition.And => Expression.AndAlso(currentComparison, subFilterExpression),
            EnumFilterCondition.Or => Expression.OrElse(currentComparison, subFilterExpression),
            _ => currentComparison
        };
    }

    private static bool IsCompatibleType(Type propertyType, object value)
    {
        if (value == null)
            return !propertyType.IsValueType || Nullable.GetUnderlyingType(propertyType) != null;

        if (propertyType.IsEnum)
            return true;

        if (Nullable.GetUnderlyingType(propertyType) != null)
            propertyType = Nullable.GetUnderlyingType(propertyType)!;

        return true; // propertyType.IsAssignableFrom(value.GetType());
    }

    private static object? ConvertValue(Type propertyType, string value)
    {
        if (value == null) return null;

        if (propertyType.IsEnum)
        {
            if (int.TryParse(value, out int result))
                return result;
            else
                return 0;
        }

        return Convert.ChangeType(value, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
    }

    private static BinaryExpression? HandleBetween(MemberExpression property, FilterItem filter)
    {
        if (!IsCompatibleType(property.Type, filter.MinValue!) || !IsCompatibleType(property.Type, filter.MaxValue!))
            return null;

        var minValue = ConvertValue(property.Type, filter.MinValue!);
        var maxValue = ConvertValue(property.Type, filter.MaxValue!);

        if (property.Type.IsEnum)
        {
            var convertedProperty = Expression.Convert(property, typeof(int));

            minValue = Convert.ToInt32(minValue);
            maxValue = Convert.ToInt32(maxValue);

            return Expression.AndAlso(
                Expression.GreaterThanOrEqual(convertedProperty, Expression.Constant(minValue, typeof(int))),
                Expression.LessThanOrEqual(convertedProperty, Expression.Constant(maxValue, typeof(int)))
            );
        }

        return Expression.AndAlso(
            Expression.GreaterThanOrEqual(property, Expression.Constant(minValue, property.Type)),
            Expression.LessThanOrEqual(property, Expression.Constant(maxValue, property.Type))
        );
    }

    private static MethodCallExpression HandleListContains(Expression property, FilterItem filter)
    {
        var convertedProperty = property.Type.IsEnum
            ? Expression.Convert(property, typeof(int))
            : property;

        var genericListType = typeof(List<>).MakeGenericType(convertedProperty.Type);
        var typedList = (IList)Activator.CreateInstance(genericListType)!;

        foreach (var item in filter.ListValue!)
        {
            object convertedItem = ConvertValue(property.Type, item)!;
            typedList.Add(convertedItem);
        }

        var list = Expression.Constant(typedList);

        var containsMethod = typeof(Enumerable).GetMethods()
            .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
            .MakeGenericMethod(convertedProperty.Type);

        return Expression.Call(containsMethod, list, convertedProperty);
    }

    private static UnaryExpression HandleListNotContains(Expression property, FilterItem filter)
    {
        return Expression.Not(HandleListContains(property, filter));
    }

    private static bool IsValidForFilterType(Type propertyType, FilterItem filter)
    {
        var listFilterIgnoreType = new HashSet<EnumFilterSearchType>
        {
            EnumFilterSearchType.Between,
            EnumFilterSearchType.ListContains,
            EnumFilterSearchType.ListNotContains
        };

        if (!IsValidSearchTypeForType(propertyType, filter.SearchType) || (!listFilterIgnoreType.Contains(filter.SearchType) && !IsCompatibleType(propertyType, filter.Value!)))
            return false;

        return true;
    }


    private static bool IsValidSearchTypeForType(Type propertyType, EnumFilterSearchType searchType)
    {
        var listNumberAndDateSearchType = new HashSet<EnumFilterSearchType>
        {
            EnumFilterSearchType.GreaterThan,
            EnumFilterSearchType.LessThan,
            EnumFilterSearchType.GreaterThanOrEqual,
            EnumFilterSearchType.LessThanOrEqual,
            EnumFilterSearchType.Between
        };

        var listStringSearchType = new HashSet<EnumFilterSearchType>
        {
            EnumFilterSearchType.Contains,
            EnumFilterSearchType.StartsWith,
            EnumFilterSearchType.EndsWith
        };

        var listGeneralSearchType = new HashSet<EnumFilterSearchType>
        {
            EnumFilterSearchType.IsNull,
            EnumFilterSearchType.IsNotNull,
            EnumFilterSearchType.Equals,
            EnumFilterSearchType.NotEquals,
            EnumFilterSearchType.ListContains,
            EnumFilterSearchType.ListNotContains,
        };

        if (listNumberAndDateSearchType.Contains(searchType))
            return IsNumericType(propertyType) || propertyType == typeof(DateTime) || propertyType.IsEnum;

        if (listStringSearchType.Contains(searchType))
            return propertyType == typeof(string);

        if (listGeneralSearchType.Contains(searchType))
            return true;

        return false;
    }

    private static bool IsNumericType(Type type)
    {
        return type == typeof(int) || type == typeof(long) || type == typeof(float) ||
               type == typeof(double) || type == typeof(decimal) || type == typeof(short) ||
               (Nullable.GetUnderlyingType(type) != null && IsNumericType(Nullable.GetUnderlyingType(type)!));
    }
}
