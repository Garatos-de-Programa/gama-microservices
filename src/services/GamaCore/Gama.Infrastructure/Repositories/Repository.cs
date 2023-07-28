using System.Linq.Expressions;
using Gama.Domain.Interfaces.Repositories;
using Gama.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Gama.Infrastructure.Repositories;

internal abstract class Repository<T> : IRepository<T> where T : class
{
    protected readonly DbContext _context;

    public Repository(GamaCoreDbContext dbContext)
    {
        _context = dbContext;
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public IQueryable<T> FindAll()
    {
        var query = _context.Set<T>().AsQueryable();

        return query;
    }

    public virtual async Task<T?> FindOneAsync<TId>(TId id) where TId : struct
    {
        return await _context.Set<T>().FindAsync(id).AsTask();
    }

    public virtual async Task InsertAsync(T tObject)
    {
        await _context.Set<T>().AddAsync(tObject);
    }

    public virtual Task Patch(T tObject)
    {
        var set = _context.Set<T>();
        set.Attach(tObject);
        _context.Entry(tObject).State = EntityState.Modified;
        return Task.CompletedTask;
    }
}