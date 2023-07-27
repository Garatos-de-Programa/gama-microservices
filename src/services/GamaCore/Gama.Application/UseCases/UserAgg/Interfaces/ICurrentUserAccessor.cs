using Gama.Domain.Models.Users;

namespace Gama.Application.UseCases.UserAgg.Interfaces
{
    public interface ICurrentUserAccessor
    {
        User GetUser();
        int GetUserId();
        string GetUsername();
    }
}
