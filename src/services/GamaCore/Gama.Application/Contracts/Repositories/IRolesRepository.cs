using Gama.Domain.Entities;

namespace Gama.Application.Contracts.Repositories
{
    public interface IRolesRepository : IRepository<Role>
    {
        public Task<IEnumerable<Role>> GetByIdsAsync(IEnumerable<byte> ids);
    }
}
