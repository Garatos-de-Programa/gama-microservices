using Gama.Domain.Interfaces.Repositories;

namespace Gama.Domain.Entities.UsersAgg
{
    public interface IRolesRepository : IRepository<Role>
    {
        public Task<IEnumerable<Role>> GetByIdsAsync(IEnumerable<byte> ids);
    }
}
