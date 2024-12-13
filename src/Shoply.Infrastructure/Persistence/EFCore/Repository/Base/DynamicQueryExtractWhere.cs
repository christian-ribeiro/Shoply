using System.Linq.Expressions;

namespace Shoply.Infrastructure.Persistence.EFCore.Repository.Base;

public static class DynamicQueryExtractWhere
{
    public static List<string> ExtractWhereProperties<T>(this IQueryable<T> query)
    {
        var propertyNames = new List<string>();
        if (query.Expression is MethodCallExpression methodCallExpression)
        {
            if (methodCallExpression.Method.Name == "Where")
            {
                if (methodCallExpression.Arguments[1] is UnaryExpression { Operand: LambdaExpression lambda })
                    ExtractPropertyNames(lambda.Body, propertyNames, lambda.Parameters[0]);
            }
        }
        return propertyNames;
    }

    private static void ExtractPropertyNames(Expression expression, List<string> propertyNames, ParameterExpression parameter)
    {
        switch (expression)
        {
            case BinaryExpression binaryExpression:
                ExtractPropertyNames(binaryExpression.Left, propertyNames, parameter);
                ExtractPropertyNames(binaryExpression.Right, propertyNames, parameter);
                break;

            case MemberExpression memberExpression:
                if (IsParameterProperty(memberExpression, parameter))
                {
                    var propertyPath = GetPropertyPath(memberExpression);
                    if (!string.IsNullOrEmpty(propertyPath))
                        propertyNames.Add(propertyPath);
                }
                break;

            case MethodCallExpression methodCallExpression:
                foreach (var argument in methodCallExpression.Arguments)
                    ExtractPropertyNames(argument, propertyNames, parameter);
                break;

            case UnaryExpression unaryExpression:
                ExtractPropertyNames(unaryExpression.Operand, propertyNames, parameter);
                break;
        }
    }

    private static bool IsParameterProperty(MemberExpression memberExpression, ParameterExpression parameter)
    {
        var current = memberExpression.Expression;
        while (current is MemberExpression parentMember)
            current = parentMember.Expression;
        return current == parameter;
    }

    private static string GetPropertyPath(MemberExpression memberExpression)
    {
        var path = memberExpression.Member.Name;
        var parent = memberExpression.Expression as MemberExpression;

        while (parent != null)
        {
            path = $"{parent.Member.Name}.{path}";
            parent = parent.Expression as MemberExpression;
        }
        return path;
    }
}