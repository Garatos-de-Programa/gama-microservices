using System.Linq.Expressions;

namespace Gama.Application.Contracts.Repositories;

public interface IRepository<T>
{
    Task InsertAsync(T tObject);
    Task<T?> FindOneAsync<TId>(TId id) where TId : struct;
    IQueryable<T> FindAll();
    Task CommitAsync();
    Task Patch(T tObject);
}