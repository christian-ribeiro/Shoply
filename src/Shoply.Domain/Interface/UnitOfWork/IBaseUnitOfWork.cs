namespace Shoply.Domain.Interface.UnitOfWork;

public interface IBaseUnitOfWork
{
    Task CommitAsync();
}