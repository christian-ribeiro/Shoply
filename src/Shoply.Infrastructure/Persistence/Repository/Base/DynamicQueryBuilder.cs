using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shoply.Infrastructure.Persistence.Repository.Base;

public class DynamicQueryBuilder<TEntity>
{
    public static async Task<List<TEntity>> GetDynamic(IQueryable<TEntity> queryable, List<string> properties)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var selector = BuildSelector(parameter, typeof(TEntity), properties);
        var lambda = Expression.Lambda<Func<TEntity, TEntity>>(selector, parameter);
        return await queryable.Select(lambda).ToListAsync();
    }

    private static Expression BuildSelector(Expression parameter, Type entityType, List<string> properties)
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

            var propertyExpression = BuildPropertyExpression(parameter, propertyName);

            if (propertyExpression != null)
            {
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
                    bindings.Add(Expression.Bind(propertyExpression.Member, toListCall));
                }
                else if (subProperties.Count > 0)
                {
                    var nestedSelector = BuildSelector(propertyExpression, propertyExpression.Type, subProperties);
                    var newExpression = Expression.New(propertyExpression.Type);
                    var memberInit = Expression.MemberInit(newExpression, ((MemberInitExpression)nestedSelector).Bindings);
                    bindings.Add(Expression.Bind(propertyExpression.Member, memberInit));
                }
                else
                {
                    bindings.Add(Expression.Bind(propertyExpression.Member, propertyExpression));
                }
            }
        }

        var newEntity = Expression.New(entityType);
        return Expression.MemberInit(newEntity, bindings);
    }

    private static MemberExpression? BuildPropertyExpression(Expression parameter, string propertyName)
    {
        var properties = propertyName.Split('.');
        Expression propertyExpression = parameter;

        foreach (var prop in properties)
        {
            if (IsCollection(propertyExpression.Type))
            {
                var elementType = propertyExpression.Type.GetGenericArguments()[0];
                propertyExpression = Expression.Parameter(elementType);
            }

            var propInfo = propertyExpression.Type.GetProperty(prop);
            if (propInfo != null)
                propertyExpression = Expression.Property(propertyExpression, propInfo);
            else
                return null;
        }

        return (MemberExpression)propertyExpression;
    }

    private static bool IsCollection(Type type) => type.IsGenericType && typeof(List<>).IsAssignableFrom(type.GetGenericTypeDefinition());
}