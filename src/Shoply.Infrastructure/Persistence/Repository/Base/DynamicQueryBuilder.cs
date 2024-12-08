using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shoply.Infrastructure.Persistence.Repository.Base;

public class DynamicQueryBuilder<TEntity>
{
    public static async Task<List<TEntity>> GetDynamic(IQueryable<TEntity> queryable, List<string> properties)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var bindings = new List<MemberBinding>();
        var collectionBindings = new Dictionary<string, LambdaExpression>();

        foreach (var property in properties)
        {
            var propertySplit = property.Split('.');
            if (propertySplit.Length > 1)
            {
                var collectionName = propertySplit[0];
                var collectionProperty = string.Join('.', propertySplit.Skip(1));

                if (!collectionBindings.ContainsKey(collectionName))
                {
                    collectionBindings[collectionName] = BuildCollectionSelector(parameter, collectionName, properties.Where(p => p.StartsWith(collectionName + ".")).ToList());
                }
            }
            else
            {
                var propertyExpression = BuildPropertyExpression(parameter, property);
                if (propertyExpression != null)
                {
                    bindings.Add(Expression.Bind(propertyExpression.Member, propertyExpression));
                }
            }
        }

        foreach (var collectionBinding in collectionBindings)
        {
            var collectionProperty = BuildPropertyExpression(parameter, collectionBinding.Key);
            var selectMethod = typeof(Enumerable)
                .GetMethods()
                .First(m => m.Name == "Select" && m.GetParameters().Length == 2)
                .MakeGenericMethod(collectionProperty.Type.GetGenericArguments()[0], collectionBinding.Value.ReturnType);

            var selectCall = Expression.Call(selectMethod, collectionProperty, collectionBinding.Value);
            var toListMethod = typeof(Enumerable)
                .GetMethod("ToList")
                .MakeGenericMethod(collectionBinding.Value.ReturnType);

            var toListCall = Expression.Call(toListMethod, selectCall);

            bindings.Add(Expression.Bind(typeof(TEntity).GetProperty(collectionBinding.Key)!, toListCall));
        }

        var selector = Expression.Lambda<Func<TEntity, TEntity>>(
            Expression.MemberInit(Expression.New(typeof(TEntity)), bindings), parameter);

        return await queryable.Select(selector).ToListAsync();
    }

    private static MemberExpression BuildPropertyExpression(Expression parameter, string propertyName)
    {
        var properties = propertyName.Split('.');
        Expression propertyExpression = parameter;

        foreach (var prop in properties)
        {
            var propInfo = propertyExpression.Type.GetProperty(prop);
            if (propInfo != null)
                propertyExpression = Expression.Property(propertyExpression, propInfo);
            else
                throw new ArgumentException($"A propriedade '{prop}' não foi encontrada.");
        }

        return (MemberExpression)propertyExpression;
    }

    private static LambdaExpression BuildCollectionSelector(Expression parameter, string collectionName, List<string> collectionProperties)
    {
        var collectionProperty = BuildPropertyExpression(parameter, collectionName);
        var elementType = collectionProperty.Type.GetGenericArguments()[0];

        var elementParameter = Expression.Parameter(elementType, "e");
        var bindings = new List<MemberBinding>();

        foreach (var property in collectionProperties)
        {
            var subPropertyName = property.Substring(collectionName.Length + 1);
            var propertyExpression = BuildPropertyExpression(elementParameter, subPropertyName);
            bindings.Add(Expression.Bind(propertyExpression.Member, propertyExpression));
        }

        var body = Expression.MemberInit(Expression.New(elementType), bindings);
        return Expression.Lambda(body, elementParameter);
    }
}