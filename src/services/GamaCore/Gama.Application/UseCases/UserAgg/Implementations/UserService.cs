using Gama.Application.Contracts.Repositories;
using Gama.Application.Seedworks.Pagination;
using Gama.Application.UseCases.UserAgg.Commands;
using Gama.Application.UseCases.UserAgg.Interfaces;
using Gama.Application.UseCases.UserAgg.Queries;
using Gama.Domain.Exceptions;
using Gama.Domain.Models.Users;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.UserAgg.Implementations;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(
        IUserRepository userRepository
        )
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> CreateAsync(User user)
    {
        var existingUser = await _userRepository.GetAsync(user.Email!, user.Username!);

        if (existingUser is not null)
        {
            return new Result<User>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Já existe um usuário com o seguinte Email ou Username"
            }));
        }

        user.PrepareToInsert();

        await _userRepository.InsertAsync(user);
        await _userRepository.CommitAsync();

        return user;
    }

    public async Task<Result<User>> UpdateAsync(int userId, UpdateUserCommand updateUserCommand)
    {
        var user = await _userRepository.FindOneAsync(userId);

        if (user is null)
        {
            return new Result<User>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Usuário nao encontrado"
            }));
        }

        user.Update(
            updateUserCommand.FirstName,
            updateUserCommand.LastName,
            updateUserCommand.DocumentNumber
            );

        await _userRepository.Patch(user);
        await _userRepository.CommitAsync();

        return user;
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var user = await _userRepository.FindOneAsync(id);

        if (user is null)
        {
            return new Result<bool>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Usuário nao encontrado"
            }));
        }

        user.Delete();

        await _userRepository.Patch(user);
        await _userRepository.CommitAsync();

        return true;
    }

    public async Task<Result<User>> GetAsync(int userId)
    {
        var user = await _userRepository.FindOneAsync(userId);
        if (user is null)
        {
            return new Result<User>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Usuário nao encontrado"
            }));
        }

        return user;
    }

    public async Task<Result<User>> UpdatePasswordAsync(UpdatePasswordCommand command)
    {
        var user = await _userRepository.GetAsync(command.Login!);
        if (user is null)
        {
            return new Result<User>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Usuário ou senha inválidos"
            }));
        }

        var isValidPassword = user.IsValidPassword(command.OldPassword!);
        if (!isValidPassword)
        {
            return new Result<User>(new ValidationException(new ValidationError()
            { PropertyName = "user", ErrorMessage = "Usuário ou senha inválidos" }));
        }

        user.ChangePassword(command.NewPassword!);

        await _userRepository.Patch(user);
        await _userRepository.CommitAsync();

        return user;
    }

    public async Task<Result<OffsetPage<User>>> GetAsync(SearchUserQuery searchUserQuery)
    {
        var users = await _userRepository.GetAsync(searchUserQuery.Size, searchUserQuery.PageNumber, searchUserQuery.Role);

        return users;
    }
}