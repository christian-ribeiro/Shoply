using Shoply.Arguments.Argument.General.Filter;
using Shoply.Arguments.Enum.General.Filter;
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

            if (!IsValidOperatorForType(propertyType, filter.Operator) || (filter.Operator != EnumFilterOperator.Between && filter.Operator != EnumFilterOperator.ListContains && filter.Operator != EnumFilterOperator.ListNotContains && !IsCompatibleType(propertyType, filter.Value!)))
                continue;

            Expression comparison = filter.Operator switch
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
                EnumFilterOperator.Between => HandleBetweenOperator(property, propertyType, filter),
                EnumFilterOperator.ListContains => HandleListContainsOperator(property, propertyType, filter),
                EnumFilterOperator.ListNotContains => HandleListNotContainsOperator(property, propertyType, filter),
                _ => throw new InvalidOperationException($"Operator {filter.Operator} not supported")
            };

            body = body == null ? comparison : Expression.AndAlso(body, comparison);
        }

        return Expression.Lambda<Func<TOutput, bool>>(body!, parameter);
    }

    private static bool IsCompatibleType(Type propertyType, object value)
    {
        if (value == null) return !propertyType.IsValueType || Nullable.GetUnderlyingType(propertyType) != null;

        var valueType = value.GetType();

        if (propertyType.IsEnum)
        {
            if (valueType == typeof(int))
            {
                return Enum.IsDefined(propertyType, value);
            }

            try
            {
                var enumValue = Convert.ToInt32(value);
                Enum.ToObject(propertyType, enumValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        if (Nullable.GetUnderlyingType(propertyType) != null)
            propertyType = Nullable.GetUnderlyingType(propertyType)!;

        return propertyType.IsAssignableFrom(valueType);
    }

    private static object? ConvertValue(Type propertyType, object value)
    {
        if (value == null) return null;
        if (propertyType.IsEnum)
        {
            var enumValue = Convert.ToInt32(value);
            return Enum.ToObject(propertyType, enumValue);
        }
        return Convert.ChangeType(value, Nullable.GetUnderlyingType(propertyType) ?? propertyType);
    }

    private static BinaryExpression HandleBetweenOperator(MemberExpression property, Type propertyType, FilterCriteria filter)
    {
        if (!IsCompatibleType(propertyType, filter.MinValue!) || !IsCompatibleType(propertyType, filter.MaxValue!))
            throw new InvalidOperationException($"Values {filter.MinValue} and {filter.MaxValue} are not compatible with property {property.Member.Name} of type {propertyType.Name}");

        var minValue = ConvertValue(propertyType, filter.MinValue!);
        var maxValue = ConvertValue(propertyType, filter.MaxValue!);

        return Expression.AndAlso(
            Expression.GreaterThanOrEqual(property, Expression.Constant(minValue, propertyType)),
            Expression.LessThanOrEqual(property, Expression.Constant(maxValue, propertyType))
        );
    }

    private static MethodCallExpression HandleListContainsOperator(Expression property, Type propertyType, FilterCriteria filter)
    {
        var listType = typeof(List<>).MakeGenericType(propertyType);
        var list = Expression.Constant(filter.ListValue);
        var containsMethod = listType.GetMethod("Contains", [propertyType]);
        return Expression.Call(list, containsMethod!, property);
    }

    private static UnaryExpression HandleListNotContainsOperator(Expression property, Type propertyType, FilterCriteria filter)
    {
        var listType = typeof(List<>).MakeGenericType(propertyType);
        var list = Expression.Constant(filter.ListValue);
        var containsMethod = listType.GetMethod("Contains", [propertyType]);
        var containsExpression = Expression.Call(list, containsMethod!, property);
        return Expression.Not(containsExpression);
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
