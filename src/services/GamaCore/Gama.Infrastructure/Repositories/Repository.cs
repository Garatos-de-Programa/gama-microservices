using System.Linq.Expressions;
using Gama.Application.Contracts.Repositories;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories;

internal abstract class Repository<T> : IRepository<T> where T : class
{
    private readonly DbContext _context;

    public Repository(GamaCoreDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<T> DeleteAsync<TId>(TId id)
    {
        var entityToBeDeleted = await _context.Set<T>().FindAsync(id);
        Delete(entityToBeDeleted);
        return entityToBeDeleted;
    }

    public IQueryable<T> FindAll()
    {
        var query = _context.Set<T>().AsQueryable();

        return query;
    }

    public async Task<T?> FindOneAsync(Expression<Func<T, bool>> filter)
    {
        return await _context.Set<T>().FindAsync(filter).AsTask();
    }

    public async Task InsertAsync(T tObject)
    {
        await _context.Set<T>().AddAsync(tObject);
    }

    public void Patch(T tObject)
    {
        var set = _context.Set<T>();
        set.Attach(tObject);
        _context.Entry(tObject).State = EntityState.Modified;
    }

    protected virtual void Delete(T entityToDelete)
    {
        var set = _context.Set<T>();
        if (_context.Entry(entityToDelete).State == EntityState.Detached)
        {
            set.Attach(entityToDelete);
        }

        set.Remove(entityToDelete);
    }
}