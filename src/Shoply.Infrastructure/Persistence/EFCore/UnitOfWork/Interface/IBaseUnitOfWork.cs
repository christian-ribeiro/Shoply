namespace Shoply.Domain.Interface.UnitOfWork;

public interface IBaseUnitOfWork
{
    Task BeginTransactionAsync();
    Task CommitAsync();
}