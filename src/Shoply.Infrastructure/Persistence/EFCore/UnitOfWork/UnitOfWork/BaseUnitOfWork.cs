using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Shoply.Domain.Interface.UnitOfWork;

namespace Shoply.Infrastructure.Persistence.EFCore.UnitOfWork;

public abstract class BaseUnitOfWork<TContext>(TContext context) : IBaseUnitOfWork
where TContext : DbContext
{
    private readonly TContext _context = context;
    private IDbContextTransaction? _transaction;

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync();
    }

    public async Task CommitAsync()
    {
        if (_transaction == null)
            throw new InvalidOperationException("A transação não foi iniciada. Certifique-se de chamar BeginTransactionAsync primeiro.");

        try
        {
            await _context.SaveChangesAsync();
            await _transaction.CommitAsync();
        }
        catch
        {
            await _transaction.RollbackAsync();
            throw;
        }
        finally
        {
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
}