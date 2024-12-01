﻿using Shoply.Arguments.Argument.Base;
using Shoply.Domain.DTO.Base;
using Shoply.Infrastructure.Persistence.Entity.Module.Registration;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shoply.Infrastructure.Entity.Base;

public abstract class BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TEntity : BaseEntity<TEntity, TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInputCreate : BaseInputCreate<TInputCreate>
    where TInputUpdate : BaseInputUpdate<TInputUpdate>
    where TOutput : BaseOutput<TOutput>
    where TDTO : BaseDTO<TInputCreate, TInputUpdate, TOutput, TDTO, TInternalPropertiesDTO, TExternalPropertiesDTO, TAuxiliaryPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>
    where TExternalPropertiesDTO : BaseExternalPropertiesDTO<TExternalPropertiesDTO>
    where TAuxiliaryPropertiesDTO : BaseAuxiliaryPropertiesDTO<TAuxiliaryPropertiesDTO>
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


    #region Virtual Properties
    #region Internal
    [NotMapped]
    public virtual User? CreationUser { get; private set; }
    [NotMapped]
    public virtual User? ChangeUser { get; private set; }
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