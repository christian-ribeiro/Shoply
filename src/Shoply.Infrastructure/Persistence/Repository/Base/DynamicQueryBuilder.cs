using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Shoply.Infrastructure.Persistence.Repository.Base
{
    public class DynamicQueryBuilder<TEntity>
    {
        public static async Task<List<TEntity>> GetDynamic(IQueryable<TEntity> queryable, List<string> properties)
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");
            var bindings = new List<MemberBinding>();
            var navigateBindings = new Dictionary<(string NavigatonProperty, Type Type), List<MemberBinding>>();

            foreach (var property in properties)
            {
                var propertySplit = property.Split('.');
                if (propertySplit.Count() > 1)
                {
                    var customerProperty = property.Substring($"{propertySplit[0]}.".Length);
                    var customerPropertyExpression = BuildPropertyExpression(parameter, propertySplit[0]);
                    var customerPropertyBinding = BuildCustomerPropertyExpression(customerPropertyExpression, customerProperty);

                    if (!navigateBindings.ContainsKey((propertySplit[0], customerPropertyExpression.Type)))
                        navigateBindings.Add((propertySplit[0], customerPropertyExpression.Type), []);

                    navigateBindings[(propertySplit[0], customerPropertyExpression.Type)].Add(customerPropertyBinding);
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

            if (navigateBindings.Count > 0)
            {
                foreach (var customerBinding in navigateBindings)
                {
                    var customerExpression = Expression.New(customerBinding.Key.Type);
                    var customerInit = Expression.MemberInit(customerExpression, customerBinding.Value);
                    bindings.Add(Expression.Bind(typeof(TEntity).GetProperty(customerBinding.Key.NavigatonProperty)!, customerInit));
                }
            }

            var selector = Expression.Lambda<Func<TEntity, TEntity>>(Expression.MemberInit(Expression.New(typeof(TEntity)), bindings), parameter);
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

        private static MemberBinding BuildCustomerPropertyExpression(Expression customerExpression, string customerProperty)
        {
            var customerPropertyExpression = BuildPropertyExpression(customerExpression, customerProperty);
            return Expression.Bind(customerPropertyExpression.Member, customerPropertyExpression);
        }
    }
}