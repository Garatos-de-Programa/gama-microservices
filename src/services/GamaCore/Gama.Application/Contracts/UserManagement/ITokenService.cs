using Gama.Domain.Entities;

namespace Gama.Application.Contracts.UserManagement;

public interface ITokenService
{
    string Generate(User user);
}