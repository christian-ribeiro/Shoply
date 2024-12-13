using Microsoft.EntityFrameworkCore;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Repository.Base;
using Shoply.Infrastructure.Entity.Base;
using System.Linq.Expressions;

namespace Shoply.Infrastructure.Persistence.Repository.Base;

public abstract class BaseRepository<TContext, TEntity, TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>(TContext context) : IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TContext : DbContext
    where TEntity : BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>, new()
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

    #region Read
    public async Task<TDTO> Get(long id, bool useCustomReturnProperty = false)
    {
        var query = await GetDynamicQuery(_dbSet.Where(x => x.Id == id), nameof(Get), "", useCustomReturnProperty);
        return FromEntityToDTO(await query.FirstOrDefaultAsync());
    }

    public async Task<List<TDTO>> GetListByListId(List<long> listId, bool useCustomReturnProperty = false)
    {
        var query = await GetDynamicQuery(_dbSet.Where(x => listId.Contains(x.Id)), nameof(GetListByListId), "", useCustomReturnProperty);
        return FromEntityToDTO(await query.ToListAsync());
    }

    public async Task<List<TDTO>> GetAll(bool useCustomReturnProperty = false)
    {
        var query = await GetDynamicQuery(_dbSet.AsQueryable(), nameof(GetAll), "", useCustomReturnProperty);
        return FromEntityToDTO(await query.ToListAsync());
    }

    public async Task<TDTO?> GetByIdentifier(TInputIdentifier inputIdentifier, bool useCustomReturnProperty = false)
    {
        var result = await GetListByListIdentifier([inputIdentifier], useCustomReturnProperty);
        return result.FirstOrDefault();
    }

    public async Task<List<TDTO>> GetListByListIdentifier(List<TInputIdentifier> listInputIdentifier, bool useCustomReturnProperty = false)
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

        var query = await GetDynamicQuery(_dbSet.Where(combinedExpression!), nameof(GetListByListIdentifier), "", useCustomReturnProperty);
        return FromEntityToDTO(await query.ToListAsync());
    }

    public async Task<List<TDTO>> GetDynamic(string[] fields, bool useCustomReturnProperty = false)
    {
        return FromEntityToDTO(await _dbSet.AsQueryable().GetDynamic([.. fields]).ToListAsync());
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

    protected async Task<IQueryable<TEntity>> GetDynamicQuery(IQueryable<TEntity> query, string? methodName, string? callAlias, bool useCustomReturnProperty)
    {
        List<string> properties;
        var returnProperty = useCustomReturnProperty ? SessionData.GetReturnProperty(_guidSessionDataRequest) : [];
        if (returnProperty == null || returnProperty.Count == 0)
            properties = new TEntity().GetProperties(methodName, callAlias);
        else
            properties = returnProperty;

        return await Task.FromResult(query.GetDynamic(properties));
    }
    #endregion
}