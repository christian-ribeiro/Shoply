using Shoply.Arguments.Utils;

namespace Shoply.Domain.DTO.Base;

public class BaseInternalPropertiesDTO<TInternalPropertiesDTO> : BaseSetProperty<TInternalPropertiesDTO>
    where TInternalPropertiesDTO : BaseInternalPropertiesDTO<TInternalPropertiesDTO>, new()
{
    public virtual long Id { get; private set; }
    public virtual DateTime? CreationDate { get; private set; }
    public virtual long? CreationUserId { get; private set; }
    public virtual DateTime? ChangeDate { get; private set; }
    public virtual long? ChangeUserId { get; private set; }

    public TInternalPropertiesDTO SetInternalData(TInternalPropertiesDTO? internalPropertiesDTO)
    {
        if (internalPropertiesDTO == null)
            return (TInternalPropertiesDTO)this;

        Id = internalPropertiesDTO.Id;
        CreationDate = internalPropertiesDTO.CreationDate;
        CreationUserId = internalPropertiesDTO.CreationUserId;
        ChangeDate = internalPropertiesDTO.ChangeDate;
        ChangeUserId = internalPropertiesDTO.ChangeUserId;

        return (TInternalPropertiesDTO)this;
    }

    public TInternalPropertiesDTO SetInternalData(long id, DateTime? creationDate, DateTime? changeDate, long? creationUserId, long? changeUserId)
    {
        Id = id;
        CreationDate = creationDate;
        CreationUserId = creationUserId;
        ChangeDate = changeDate;
        ChangeUserId = changeUserId;

        return (TInternalPropertiesDTO)this;
    }

    public TInternalPropertiesDTO SetInternalData(long id)
    {
        Id = id;
        return (TInternalPropertiesDTO)this;
    }
}