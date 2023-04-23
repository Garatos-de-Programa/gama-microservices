using Gama.Application.Contracts.Repositories;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Domain.Entities;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.UserManagement;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<User>> CreateAsync(CreateUserCommand createUserCommand)
    {
        var user = await _userRepository.GetByLoginAsync(createUserCommand.Email, createUserCommand.Username);

        if (user is not null)
        {
            return new Result<User>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Já existe um usuário com o seguinte Email ou Username"
            }));
        }

        var userToInsert = createUserCommand.ToUser();

        await _userRepository.InsertAsync(userToInsert);
        await _userRepository.CommitAsync();

        return userToInsert;
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

        _userRepository.Patch(user);
        await _userRepository.CommitAsync();

        return user;
    }

    public async Task<Result<bool>> DeleteAsync(int id)
    {
        var user = await _userRepository.DeleteAsync(id);
       
        if (user is null)
        {
            return new Result<bool>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Usuário nao encontrado"
            }));
        }

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
        var user = await _userRepository.GetByLoginAsync(command.Login);
        if (user is null)
        {
            return new Result<User>(new ValidationException(new ValidationError()
            {
                PropertyName = "User",
                ErrorMessage = "Usuário ou senha inválidos"
            }));
        }

        var isValidPassword = user.IsValidPassword(command.OldPassword);
        if (!isValidPassword)
        {
            return new Result<User>(new ValidationException(new ValidationError()
                { PropertyName = "user", ErrorMessage = "Usuário ou senha inválidos" }));
        }
        
        user.ChangePassword(command.NewPassword);
        
        _userRepository.Patch(user);
        await _userRepository.CommitAsync();

        return user;
    }
}