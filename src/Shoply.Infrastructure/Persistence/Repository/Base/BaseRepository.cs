using Microsoft.EntityFrameworkCore;
using Shoply.Arguments.Argument.Base;
using Shoply.Arguments.Argument.General.Session;
using Shoply.Domain.DTO.Base;
using Shoply.Domain.Interface.Repository.Base;
using Shoply.Infrastructure.Entity.Base;

namespace Shoply.Infrastructure.Persistence.Repository.Base;

public class BaseRepository<TContext, TEntity, TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO> : IBaseRepository<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TContext : DbContext
    where TEntity : BaseEntity<TEntity, TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TInputIdentifier : BaseInputIdentifier<TInputIdentifier>, new()
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    protected Guid _guidSessionDataRequest;
    protected readonly TContext _context;
    protected DbSet<TEntity> _dbSet;

    protected BaseRepository(TContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    #region Internal
    public void SetGuid(Guid guidSessionDataRequest)
    {
        _guidSessionDataRequest = guidSessionDataRequest;
        SessionHelper.SetGuidSessionDataRequest(this, guidSessionDataRequest);
    }

    internal static TEntity FromDTOToEntity(TDTO dto)
    {
        return SessionData.Mapper!.MapperEntityDTO.Map<TDTO, TEntity>(dto);
    }

    internal static TDTO FromEntityToDTO(TEntity Entity)
    {
        return SessionData.Mapper!.MapperEntityDTO.Map<TEntity, TDTO>(Entity);
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