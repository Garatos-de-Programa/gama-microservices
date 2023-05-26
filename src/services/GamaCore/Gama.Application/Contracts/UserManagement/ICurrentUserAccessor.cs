using Gama.Domain.Models.Users;

namespace Gama.Application.Contracts.UserManagement
{
    public interface ICurrentUserAccessor
    {
        User GetUser();
        int GetUserId();
        string GetUsername();
    }
}
