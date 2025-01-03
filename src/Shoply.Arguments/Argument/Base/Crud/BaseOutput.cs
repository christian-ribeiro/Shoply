using Shoply.Arguments.Argument.Module.Registration;

namespace Shoply.Arguments.Argument.Base;

public class BaseOutput<TOutput> where TOutput : BaseOutput<TOutput>
{
    public long Id { get; set; }
    public virtual DateTime? CreationDate { get; set; }
    public virtual long? CreationUserId { get; set; }
    public virtual DateTime? ChangeDate { get; set; }
    public virtual long? ChangeUserId { get; set; }
    public virtual OutputUser? CreationUser { get; set; }
    public virtual OutputUser? ChangeUser { get; set; }

    public TOutput SetInternalData(long id, DateTime? creationDate, DateTime? changeDate, long? creationUserId, long? changeUserId, OutputUser? creationUser, OutputUser? changeUser)
    {
        Id = id;
        CreationDate = creationDate;
        CreationUserId = creationUserId;
        ChangeDate = changeDate;
        ChangeUserId = changeUserId;
        CreationUser = creationUser;
        ChangeUser = changeUser;

        return (TOutput)this;
    }
}