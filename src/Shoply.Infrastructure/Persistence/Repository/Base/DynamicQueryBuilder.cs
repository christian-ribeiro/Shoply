using System.Linq.Expressions;
using System.Reflection;

namespace Shoply.Infrastructure.Persistence.Repository.Base
{
    public class DynamicQueryBuilder<T>
    {
        public async Task<IQueryable<TEntity>> QueryWithDynamicProjectionAsync<TEntity>(
    IQueryable<TEntity> query,
    string[] properties)
    where TEntity : class
        {
            var parameter = Expression.Parameter(typeof(TEntity), "x");

            var bindings = new List<MemberBinding>();

            foreach (var property in properties)
            {
                var parts = property.Split('.'); // Divide por "." para identificar navegação
                var memberExpression = GetMemberExpression(parameter, parts);
                var memberExpressionType = GetMemberExpression(parameter, parts.Take(1).ToArray());
                var propertyInfo = memberExpressionType.Member as PropertyInfo; // Garantir que é uma PropertyInfo

                // Se a propriedade for do tipo de navegação, precisamos criar um novo objeto para a navegação.
                if (parts.Length == 1)
                {
                    // Se for uma propriedade simples (não navegação), apenas adiciona a projeção direta
                    bindings.Add(Expression.Bind(propertyInfo, memberExpression));
                }
                else
                {
                    // Se for uma propriedade de navegação, cria a projeção da navegação
                    var navigationMemberInit = CreateNavigationMemberInit(parameter, propertyInfo, memberExpression, parts.Skip(1).ToArray());
                    bindings.Add(Expression.Bind(propertyInfo, navigationMemberInit));
                }
            }

            var body = Expression.MemberInit(Expression.New(typeof(TEntity)), bindings);
            var selector = Expression.Lambda<Func<TEntity, TEntity>>(body, parameter);

            return query.Select(selector);
        }

        private static MemberExpression GetMemberExpression(Expression parameter, string[] parts)
        {
            Expression currentExpression = parameter;

            foreach (var part in parts)
            {
                var propertyInfo = currentExpression.Type.GetProperty(part);

                if (propertyInfo == null)
                {
                    throw new InvalidOperationException($"Propriedade '{part}' não encontrada.");
                }

                currentExpression = Expression.Property(currentExpression, propertyInfo);
            }

            return (MemberExpression)currentExpression;
        }

        private static MemberInitExpression CreateNavigationMemberInit(Expression parameter, PropertyInfo propertyInfo, Expression navigationExpression, string[] remainingParts)
        {
            // Se a propriedade for uma navegação, fazemos uma verificação mais detalhada.
            if (remainingParts.Length > 0)
            {
                // Se houver mais partes, significa que temos uma propriedade dentro do objeto de navegação.
                var nextPart = remainingParts[0];
                var nextPropertyInfo = propertyInfo.PropertyType.GetProperty(nextPart);

                if (nextPropertyInfo == null)
                {
                    throw new InvalidOperationException($"Propriedade '{nextPart}' não encontrada dentro de '{propertyInfo.Name}'.");
                }

                var nextMemberExpression = Expression.Property(navigationExpression, nextPropertyInfo);
                var nextRemainingParts = remainingParts.Skip(1).ToArray();

                // Chama recursivamente para as próximas propriedades dentro da navegação
                return CreateNavigationMemberInit(parameter, nextPropertyInfo, nextMemberExpression, nextRemainingParts);
            }
            else
            {
                // Caso contrário, estamos no final da navegação e podemos projetar a propriedade diretamente.
                return Expression.MemberInit(Expression.New(propertyInfo.PropertyType));
            }
        }
    }
}