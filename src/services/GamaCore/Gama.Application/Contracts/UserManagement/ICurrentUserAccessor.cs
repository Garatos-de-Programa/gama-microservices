using Gama.Domain.Entities;

namespace Gama.Application.Contracts.UserManagement
{
    public interface ICurrentUserAccessor
    {
        User GetUser();
        int GetUserId();
        string GetUsername();
    }
}
