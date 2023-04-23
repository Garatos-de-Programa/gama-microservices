using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Domain.Entities;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.UserManagement;

public interface IUserService
{
    Task<Result<User>> CreateAsync(CreateUserCommand user);
    Task<Result<User>> UpdateAsync(int userId, UpdateUserCommand user);
    Task<Result<bool>> DeleteAsync(int id);
    Task<Result<User>> GetAsync(int userId);
    Task<Result<User>> UpdatePasswordAsync(UpdatePasswordCommand command);
}