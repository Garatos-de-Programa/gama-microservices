using Gama.Domain.Entities.UsersAgg;

namespace Gama.Application.UseCases.UserAgg.Interfaces
{
    public interface ICurrentUserAccessor
    {
        User GetUser();
        int GetUserId();
        string GetUsername();
    }
}
