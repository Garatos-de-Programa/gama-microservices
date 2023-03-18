using System.Linq.Expressions;

namespace Gama.Application.Contracts.Repositories;

public interface IRepository<T>
{
    Task InsertAsync(T tObject);
    Task<T?> FindOneAsync(Expression<Func<T, bool>> filter);
    IQueryable<T> FindAll();
    void Patch(T tObject);
    Task<T> DeleteAsync<TId>(TId id);
    Task CommitAsync();
}