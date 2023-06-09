using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Application.DataContracts.Queries.UserManagement;
using Gama.Application.Seedworks.Pagination;
using Gama.Domain.Models.Users;
using Gama.Domain.ValueTypes;

namespace Gama.Application.Contracts.UserManagement;

public interface IUserService
{
    Task<Result<User>> CreateAsync(User user);
    Task<Result<User>> UpdateAsync(int userId, UpdateUserCommand user);
    Task<Result<bool>> DeleteAsync(int id);
    Task<Result<User>> GetAsync(int userId);
    Task<Result<User>> UpdatePasswordAsync(UpdatePasswordCommand command);
    Task<Result<OffsetPage<User>>> GetAsync(SearchUserQuery searchUserQuery);
}