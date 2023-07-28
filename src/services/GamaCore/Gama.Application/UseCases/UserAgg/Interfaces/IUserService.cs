using Gama.Application.Seedworks.Pagination;
using Gama.Application.UseCases.UserAgg.Commands;
using Gama.Application.UseCases.UserAgg.Queries;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.UserAgg.Interfaces;

public interface IUserService
{
    Task<Result<User>> CreateAsync(User user);
    Task<Result<User>> UpdateAsync(int userId, UpdateUserCommand user);
    Task<Result<bool>> DeleteAsync(int id);
    Task<Result<User>> GetAsync(int userId);
    Task<Result<User>> UpdatePasswordAsync(UpdatePasswordCommand command);
    Task<Result<OffsetPage<User>>> GetAsync(SearchUserQuery searchUserQuery);
}