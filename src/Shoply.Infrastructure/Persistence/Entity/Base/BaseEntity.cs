using Shoply.Arguments.Argument.Base;
using Shoply.Domain.DTO.Base;
using Shoply.Infrastructure.DataAnnotation;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoply.Infrastructure.Entity.Base;

[Entity]
public abstract class BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TEntity : BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>, new()
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>, new()
{
    public long Id { get; set; }
    [NotMapped]
    public virtual DateTime? CreationDate { get; set; }
    [NotMapped]
    public virtual long? CreationUserId { get; set; }
    [NotMapped]
    public virtual DateTime? ChangeDate { get; set; }
    [NotMapped]
    public virtual long? ChangeUserId { get; set; }


    #region Virtual Properties
    #region Internal
    [NotMapped]
    public virtual User? CreationUser { get; set; }
    [NotMapped]
    public virtual User? ChangeUser { get; set; }
    #endregion
    #endregion

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