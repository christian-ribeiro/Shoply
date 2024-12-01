using Shoply.Arguments.Argument.Base;
using Shoply.Domain.DTO.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoply.Infrastructure.Entity.Base;

public abstract class BaseEntity<TEntity, TInputCreate, TInputUpdate, TInputIdentifier, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
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
    public long Id { get; private set; }
    [NotMapped]
    public virtual DateTime? CreationDate { get; private set; }
    [NotMapped]
    public virtual long? CreationUserId { get; private set; }
    [NotMapped]
    public virtual DateTime? ChangeDate { get; private set; }
    [NotMapped]
    public virtual long? ChangeUserId { get; private set; }

    public TEntity SetInternalData(TInternalPropertiesDTO internalPropertiesDTO)
    {
        Id = internalPropertiesDTO.Id;
        CreationDate = internalPropertiesDTO.CreationDate;
        CreationUserId = internalPropertiesDTO.CreationUserId;
        ChangeDate = internalPropertiesDTO.ChangeDate;
        ChangeUserId = internalPropertiesDTO.ChangeUserId;
        return (TEntity)this;
    }

    public TEntity SetInternalData(long id, DateTime? creationDate, long? creationUserId, DateTime? changeDate, long? changeUserId)
    {
        Id = id;
        CreationDate = creationDate;
        ChangeDate = changeDate;
        CreationUserId = creationUserId;
        ChangeUserId = changeUserId;
        return (TEntity)this;
    }
}