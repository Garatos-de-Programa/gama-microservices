namespace Gama.Application.Contracts.Mappers;

public interface IEntityMapper
{
    TResult Map<TResult, TEntity>(TEntity entity);
}