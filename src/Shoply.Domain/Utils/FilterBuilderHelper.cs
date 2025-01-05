using Shoply.Arguments.Argument.General.Filter;
using Shoply.Arguments.Enum.General.Filter;
using System.Linq;
using System.Linq.Expressions;

namespace Shoply.Domain.Utils;

public static class FilterBuilderHelper
{
    public static Expression<Func<TOutput, bool>> BuildPredicate<TOutput>(List<FilterCriteria> filters)
    {
        var parameter = Expression.Parameter(typeof(TOutput), "x");
        Expression? body = null;

        foreach (var filter in filters)
        {
            var property = Expression.Property(parameter, filter.PropertyName);
            var propertyType = property.Type;

            if (!IsValidForFilterType(propertyType, filter))
                continue;

            Expression? comparison = filter.Operator switch
            {
                EnumFilterOperator.Equals => Expression.Equal(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterOperator.NotEquals => Expression.NotEqual(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterOperator.GreaterThan => Expression.GreaterThan(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterOperator.LessThan => Expression.LessThan(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(property, Expression.Constant(ConvertValue(propertyType, filter.Value!), propertyType)),
                EnumFilterOperator.Contains => Expression.Call(property, typeof(string).GetMethod("Contains", [typeof(string)])!, Expression.Constant(filter.Value)),
                EnumFilterOperator.StartsWith => Expression.Call(property, typeof(string).GetMethod("StartsWith", [typeof(string)])!, Expression.Constant(filter.Value)),
                EnumFilterOperator.EndsWith => Expression.Call(property, typeof(string).GetMethod("EndsWith", [typeof(string)])!, Expression.Constant(filter.Value)),
                EnumFilterOperator.IsNull => Expression.Equal(property, Expression.Constant(null)),
                EnumFilterOperator.IsNotNull => Expression.NotEqual(property, Expression.Constant(null)),
                EnumFilterOperator.Between => HandleBetweenOperator(property, filter),
                EnumFilterOperator.ListContains => HandleListContainsOperator(property, filter),
                EnumFilterOperator.ListNotContains => HandleListNotContainsOperator(property, filter),
                _ => null
            };

            if (comparison != null)
                body = body == null ? comparison : Expression.AndAlso(body, comparison);
        }

        return Expression.Lambda<Func<TOutput, bool>>(body!, parameter);
    }

    private static bool IsCompatibleType(Type propertyType, object value)
    {
        if (value == null)
            return !propertyType.IsValueType || Nullable.GetUnderlyingType(propertyType) != null;

        if (propertyType.IsEnum)
            return true;

        if (Nullable.GetUnderlyingType(propertyType) != null)
            propertyType = Nullable.GetUnderlyingType(propertyType)!;

        return propertyType.IsAssignableFrom(value.GetType());
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

    private static BinaryExpression? HandleBetweenOperator(MemberExpression property, FilterCriteria filter)
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

    private static MethodCallExpression HandleListContainsOperator(Expression property, FilterCriteria filter)
    {
        var typedList = filter.ListValue!
            .Select(value => property.Type.IsEnum
                ? Convert.ToInt32(value)
                : Convert.ChangeType(value, property.Type))
            .Cast<int>()
            .ToList();

        var convertedProperty = property.Type.IsEnum
            ? Expression.Convert(property, typeof(int))
            : property;

        var list = Expression.Constant(typedList);

        var containsMethod = typeof(Enumerable).GetMethods()
            .First(m => m.Name == "Contains" && m.GetParameters().Length == 2)
            .MakeGenericMethod(typeof(int));

        return Expression.Call(containsMethod, list, convertedProperty);
    }

    private static UnaryExpression HandleListNotContainsOperator(Expression property, FilterCriteria filter)
    {
        return Expression.Not(HandleListContainsOperator(property, filter));
    }

    private static bool IsValidForFilterType(Type propertyType, FilterCriteria filter)
    {
        var listFilterIgnoreType = new HashSet<EnumFilterOperator>
        {
            EnumFilterOperator.Between,
            EnumFilterOperator.ListContains,
            EnumFilterOperator.ListNotContains
        };

        if (!IsValidOperatorForType(propertyType, filter.Operator) || (!listFilterIgnoreType.Contains(filter.Operator) && !IsCompatibleType(propertyType, filter.Value!)))
            return false;

        return true;
    }


    private static bool IsValidOperatorForType(Type propertyType, EnumFilterOperator operatorType)
    {
        var listNumberAndDateOperator = new HashSet<EnumFilterOperator>
        {
            EnumFilterOperator.GreaterThan,
            EnumFilterOperator.LessThan,
            EnumFilterOperator.GreaterThanOrEqual,
            EnumFilterOperator.LessThanOrEqual,
            EnumFilterOperator.Between
        };

        var listStringOperator = new HashSet<EnumFilterOperator>
        {
            EnumFilterOperator.Contains,
            EnumFilterOperator.StartsWith,
            EnumFilterOperator.EndsWith
        };

        var listGeneralOperator = new HashSet<EnumFilterOperator>
        {
            EnumFilterOperator.IsNull,
            EnumFilterOperator.IsNotNull,
            EnumFilterOperator.Equals,
            EnumFilterOperator.NotEquals,
            EnumFilterOperator.ListContains,
            EnumFilterOperator.ListNotContains,
        };

        if (listNumberAndDateOperator.Contains(operatorType))
            return IsNumericType(propertyType) || propertyType == typeof(DateTime) || propertyType.IsEnum;

        if (listStringOperator.Contains(operatorType))
            return propertyType == typeof(string);

        if (listGeneralOperator.Contains(operatorType))
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
