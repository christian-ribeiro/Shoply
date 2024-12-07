using Shoply.Infrastructure.DataAnnotation;
using System.Dynamic;
using System.Linq.Expressions;
using System.Reflection;

namespace Shoply.Infrastructure.Persistence.Repository.Base
{
    public class DynamicQueryBuilder<T>
    {
        public static IQueryable<TEntity> BuildQuery<TEntity>(IQueryable<TEntity> source, string[] fields)
        {
            // Parâmetro da expressão (representa o "x" no "x => ...")
            var parameter = Expression.Parameter(typeof(TEntity), "x");

            // Lista de bindings para configurar a projeção dinâmica
            var bindings = new List<MemberAssignment>();

            // Percorre cada campo fornecido pelo usuário
            foreach (var field in fields)
            {
                // Divide o caminho das propriedades para lidar com navegação
                var parts = field.Split('.');
                Expression currentExpression = parameter;
                Type currentType = typeof(TEntity); // Mantemos o tipo original da entidade

                // Percorre o caminho completo das propriedades
                foreach (var part in parts)
                {
                    // Obtém a propriedade ou campo atual
                    var property = currentType.GetProperty(part);
                    if (property == null)
                    {
                        throw new InvalidOperationException($"Propriedade '{part}' não encontrada em {currentType.Name}.");
                    }

                    // Se a propriedade for de navegação, atualizamos o currentType
                    if (property.PropertyType.IsDefined(typeof(EntityAttribute)))
                    {
                        currentType = property.PropertyType;  // Atualiza para o tipo da navegação
                    }

                    // Navega pela propriedade
                    currentExpression = Expression.PropertyOrField(currentExpression, part);
                }

                // A expressão final deve ser vinculada ao tipo correto
                var finalProperty = currentType.GetProperty(parts.Last());
                if (finalProperty != null)
                {
                    // Aqui vinculamos a expressão correta à propriedade final
                    bindings.Add(Expression.Bind(finalProperty, currentExpression));
                }
            }

            // Cria a inicialização para a projeção
            var initExpression = Expression.MemberInit(Expression.New(typeof(TEntity)), bindings);

            // Cria o Lambda para a projeção
            var lambda = Expression.Lambda<Func<TEntity, TEntity>>(initExpression, parameter);

            // Aplica a projeção no IQueryable
            return source.Select(lambda);
        }
    }
}