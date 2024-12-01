using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shoply.Domain.Interface.UnitOfWork;

namespace Shoply.Infrastructure.Persistence.UnitOfWork;

public class BaseUnitOfWork<TContext>(TContext context) : IBaseUnitOfWork
where TContext : DbContext
{
    private readonly IDbContextTransaction transaction = context.Database.BeginTransaction();

    public async Task CommitAsync()
    {
        await transaction.CommitAsync();
    }
}