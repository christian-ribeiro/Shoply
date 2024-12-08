using Microsoft.EntityFrameworkCore;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Repository.Base;
using Shoply.Infrastructure.Entity.Base;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using System.Linq.Expressions;
using System.Reflection;

namespace Shoply.Infrastructure.Persistence.Repository.Base;

public abstract class BaseRepository<TContext, TEntity, TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>(TContext context) : IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TContext : DbContext
    where TEntity : BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    protected Guid _guidSessionDataRequest;
    protected readonly TContext _context = context;
    protected readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public async Task<IEnumerable<TEntity>> GetDynamic(List<string> properties)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var bindings = new List<MemberBinding>();

        // Projeção para o objeto Customer
        var customerBindings = new Dictionary<(string, Type), List<MemberBinding>>();

        foreach (var property in properties)
        {
            var propertySplit = property.Split('.');
            if (propertySplit.Count() > 1)
            {
                var customerProperty = property.Substring($"{propertySplit[0]}.".Length);
                var customerPropertyExpression = BuildPropertyExpression(parameter, propertySplit[0]);
                var customerPropertyBinding = BuildCustomerPropertyExpression(customerPropertyExpression, customerProperty);

                if (!customerBindings.ContainsKey((propertySplit[0], customerPropertyExpression.Type)))
                    customerBindings.Add((propertySplit[0], customerPropertyExpression.Type), []);

                customerBindings[(propertySplit[0], customerPropertyExpression.Type)].Add(customerPropertyBinding);
            }
            else
            {
                // A propriedade é de CustomerAddress
                var propertyExpression = BuildPropertyExpression(parameter, property);
                if (propertyExpression != null)
                {
                    bindings.Add(Expression.Bind(propertyExpression.Member, propertyExpression));
                }
            }
        }

        // Se existirem propriedades do Customer, as agregamos em um novo objeto Customer
        if (customerBindings.Any())
        {
            foreach (var customerBinding in customerBindings)
            {
                var customerExpression = Expression.New(customerBinding.Key.Item2);
                var customerInit = Expression.MemberInit(customerExpression, customerBinding.Value);
                bindings.Add(Expression.Bind(typeof(TEntity).GetProperty(customerBinding.Key.Item1), customerInit));
            }
        }

        // Criamos a projeção final
        var selector = Expression.Lambda<Func<TEntity, TEntity>>(
            Expression.MemberInit(Expression.New(typeof(TEntity)), bindings),
            parameter
        );

        return await _context.Set<TEntity>().Select(selector).ToListAsync();
    }

    private MemberExpression BuildPropertyExpression(Expression parameter, string propertyName)
    {
        var properties = propertyName.Split('.');
        Expression propertyExpression = parameter;

        // Navegar por cada parte da propriedade
        foreach (var prop in properties)
        {
            // Tenta acessar a propriedade diretamente
            var propInfo = propertyExpression.Type.GetProperty(prop);
            if (propInfo != null)
            {
                // Se for uma propriedade de navegação, usamos Expression.Property
                propertyExpression = Expression.Property(propertyExpression, propInfo);
            }
            else
            {
                throw new ArgumentException($"A propriedade '{prop}' não foi encontrada.");
            }
        }

        return (MemberExpression)propertyExpression;
    }

    private MemberBinding BuildCustomerPropertyExpression(Expression customerExpression, string customerProperty)
    {
        // Aqui, fazemos a projeção específica para as propriedades de Customer
        var customerPropertyExpression = BuildPropertyExpression(customerExpression, customerProperty);
        return Expression.Bind(customerPropertyExpression.Member, customerPropertyExpression);
    }

    #region Read
    public async Task<TDTO> Get(long id)
    {
        return FromEntityToDTO(await _dbSet.FindAsync(id));
    }

    public async Task<List<TDTO>> GetListByListId(List<long> listId)
    {
        return FromEntityToDTO(await _dbSet.Where(x => listId.Contains(x.Id)).ToListAsync());
    }

    public async Task<List<TDTO>> GetAll()
    {
        return FromEntityToDTO(await _dbSet.ToListAsync());
    }

    public async Task<TDTO?> GetByIdentifier(TInputIdentifier inputIdentifier)
    {
        var result = await GetListByListIdentifier([inputIdentifier]);
        return result.FirstOrDefault();
    }

    public async Task<List<TDTO>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier)
    {
        if (listInputIdentifier == null || listInputIdentifier.Count == 0)
            return new List<TDTO>();

        Expression<Func<TEntity, bool>>? combinedExpression = null;

        foreach (var inputIdentifier in listInputIdentifier)
        {
            Expression<Func<TEntity, bool>>? individualExpression = null;

            foreach (var property in typeof(TInputIdentifier).GetProperties())
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(inputIdentifier);

                if (propertyValue != null)
                {
                    var parameter = Expression.Parameter(typeof(TEntity), "x");
                    var member = Expression.Property(parameter, propertyName);
                    var constant = Expression.Constant(propertyValue, member.Type);

                    var body = Expression.Equal(member, constant);
                    var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);

                    individualExpression = individualExpression == null ? lambda : CombineExpressions(individualExpression, lambda);
                }
            }

            combinedExpression = combinedExpression == null
                ? individualExpression!
                : CombineExpressions(combinedExpression, individualExpression!, Expression.OrElse)!;
        }

        IQueryable<TEntity> query = _dbSet.Where(combinedExpression!);

        var entities = await query.ToListAsync();

        return FromEntityToDTO(entities);
    }

    public async Task<List<TDTO>> GetDynamic(string[] fields)
    {
        var query = _dbSet.AsQueryable();
        var projectedQuery = await GetDynamic(fields.ToList());
        return FromEntityToDTO(projectedQuery.ToList());
    }
    #endregion

    #region Create
    public async Task<TDTO?> Create(TDTO dto)
    {
        var result = await Create([dto]);
        return result?.FirstOrDefault();
    }

    public async Task<List<TDTO>> Create(List<TDTO> listDTO)
    {
        List<TEntity> listEntity = FromDTOToEntity(SetCreationData(listDTO));
        await _dbSet.AddRangeAsync(listEntity);
        await _context.SaveChangesAsync();
        return FromEntityToDTO(listEntity);
    }

    private List<TDTO> SetCreationData(List<TDTO> listDTO)
    {
        DateTime creationDate = DateTime.UtcNow;
        long? creationUserId = SessionData.GetLoggedUser(_guidSessionDataRequest)?.Id;

        _ = (from i in listDTO
             select i.InternalPropertiesDTO
             .SetProperty(nameof(i.InternalPropertiesDTO.CreationDate), creationDate)
             .SetProperty(nameof(i.InternalPropertiesDTO.CreationUserId), creationUserId)
             ).ToList();

        return listDTO;
    }
    #endregion

    #region Update
    public async Task<TDTO?> Update(TDTO dto)
    {
        var result = await Update([dto]);
        return result?.FirstOrDefault();
    }

    public async Task<List<TDTO>> Update(List<TDTO> listDTO)
    {
        _dbSet.UpdateRange(FromDTOToEntity(SetUpdateData(listDTO)));
        await _context.SaveChangesAsync();
        return listDTO;
    }

    private List<TDTO> SetUpdateData(List<TDTO> listDTO)
    {
        DateTime changeDate = DateTime.UtcNow;
        long? changeUserId = SessionData.GetLoggedUser(_guidSessionDataRequest)?.Id;

        _ = (from i in listDTO
             select i.InternalPropertiesDTO
             .SetProperty(nameof(i.InternalPropertiesDTO.ChangeDate), changeDate)
             .SetProperty(nameof(i.InternalPropertiesDTO.ChangeUserId), changeUserId)
             ).ToList();

        return listDTO;
    }
    #endregion

    #region Delete
    public async Task<bool> Delete(TDTO dto)
    {
        return await Delete([dto]);
    }

    public async Task<bool> Delete(List<TDTO> listDTO)
    {
        _dbSet.RemoveRange(FromDTOToEntity(listDTO));
        await _context.SaveChangesAsync();
        return true;
    }
    #endregion

    #region Internal
    private static Expression<Func<T, bool>> CombineExpressions<T>(
    Expression<Func<T, bool>> expr1,
    Expression<Func<T, bool>> expr2,
    Func<Expression, Expression, BinaryExpression>? combiner = null)
    {
        combiner = combiner ?? Expression.AndAlso;

        var parameter = Expression.Parameter(typeof(T), "x");

        var leftVisitor = new ReplaceParameterVisitor(expr1.Parameters[0], parameter);
        var left = leftVisitor.Visit(expr1.Body);

        var rightVisitor = new ReplaceParameterVisitor(expr2.Parameters[0], parameter);
        var right = rightVisitor.Visit(expr2.Body);

        return Expression.Lambda<Func<T, bool>>(combiner(left, right), parameter);
    }

    private class ReplaceParameterVisitor(ParameterExpression oldParameter, ParameterExpression newParameter) : ExpressionVisitor
    {
        private readonly ParameterExpression _oldParameter = oldParameter;
        private readonly ParameterExpression _newParameter = newParameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return node == _oldParameter ? _newParameter : base.VisitParameter(node);
        }
    }

    public void SetGuid(Guid guidSessionDataRequest)
    {
        _guidSessionDataRequest = guidSessionDataRequest;
        SessionHelper.SetGuidSessionDataRequest(this, guidSessionDataRequest);
    }

    internal static TEntity FromDTOToEntity(TDTO dto)
    {
        return SessionData.Mapper!.MapperEntityDTO.Map<TDTO, TEntity>(dto);
    }

    internal static TDTO FromEntityToDTO(TEntity? entity)
    {
        return SessionData.Mapper!.MapperEntityDTO.Map<TEntity, TDTO>(entity!);
    }

    internal static List<TEntity> FromDTOToEntity(List<TDTO> listDTO)
    {
        return SessionData.Mapper!.MapperEntityDTO.Map<List<TDTO>, List<TEntity>>(listDTO);
    }

    internal static List<TDTO> FromEntityToDTO(List<TEntity> listEntity)
    {
        return SessionData.Mapper!.MapperEntityDTO.Map<List<TEntity>, List<TDTO>>(listEntity);
    }
    #endregion
}