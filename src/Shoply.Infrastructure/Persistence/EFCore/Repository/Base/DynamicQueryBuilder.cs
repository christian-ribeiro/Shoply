using System.Linq.Expressions;

namespace Shoply.Infrastructure.Persistence.EFCore.Repository.Base;

public static class DynamicQueryBuilder
{
    public static IQueryable<TEntity> GetDynamic<TEntity>(this IQueryable<TEntity> queryable, List<string> properties)
    {
        var whereProperties = queryable.ExtractWhereProperties();
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var selector = BuildSelector(parameter, typeof(TEntity), properties.Union(whereProperties).ToList());
        var lambda = Expression.Lambda<Func<TEntity, TEntity>>(selector, parameter);
        return queryable.Select(lambda);
    }

    private static MemberInitExpression BuildSelector(Expression parameter, Type entityType, List<string> properties)
    {
        var bindings = new List<MemberBinding>();
        var groupedProperties = properties.GroupBy(p => p.Split('.')[0]);

        foreach (var group in groupedProperties)
        {
            var propertyName = group.Key;
            var subProperties = group
                .Where(p => p.Contains('.'))
                .Select(p => string.Join('.', p.Split('.').Skip(1)))
                .ToList();

            var propertyInfo = entityType.GetProperty(propertyName);
            if (propertyInfo == null) continue;

            var propertyExpression = Expression.Property(parameter, propertyInfo);

            if (IsCollection(propertyExpression.Type))
            {
                var elementType = propertyExpression.Type.GetGenericArguments()[0];
                var collectionParameter = Expression.Parameter(elementType, "e");
                var collectionSelector = BuildSelector(collectionParameter, elementType, subProperties);
                var collectionLambda = Expression.Lambda(collectionSelector, collectionParameter);

                var selectMethod = typeof(Enumerable)
                    .GetMethods()
                    .First(m => m.Name == "Select" && m.GetParameters().Length == 2)
                    .MakeGenericMethod(elementType, collectionSelector.Type);

                var selectCall = Expression.Call(selectMethod, propertyExpression, collectionLambda);

                var toListMethod = typeof(Enumerable)
                    .GetMethod("ToList")!
                    .MakeGenericMethod(elementType);

                var toListCall = Expression.Call(toListMethod, selectCall);
                bindings.Add(Expression.Bind(propertyInfo, toListCall));
            }
            else if (subProperties.Count > 0)
            {
                var nestedSelector = BuildSelector(propertyExpression, propertyExpression.Type, subProperties);

                var nullCheck = Expression.Equal(propertyExpression, Expression.Constant(null));
                var defaultValue = Expression.Default(propertyExpression.Type);
                var conditional = Expression.Condition(nullCheck, defaultValue, nestedSelector);

                bindings.Add(Expression.Bind(propertyInfo, conditional));
            }
            else
            {
                bindings.Add(Expression.Bind(propertyInfo, propertyExpression));
            }
        }

        var newEntity = Expression.New(entityType);
        return Expression.MemberInit(newEntity, bindings);
    }

    private static bool IsCollection(Type type) => type.IsGenericType && typeof(List<>).IsAssignableFrom(type.GetGenericTypeDefinition());
}