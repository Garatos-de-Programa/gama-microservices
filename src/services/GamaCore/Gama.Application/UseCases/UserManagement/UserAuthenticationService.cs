using Gama.Application.Contracts.Repositories;
using Gama.Application.Contracts.UserManagement;
using Gama.Application.DataContracts.Commands.UserManagement;
using Gama.Application.DataContracts.Responses.UserManagement;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Application.UseCases.UserManagement;

public class UserAuthenticationService : IUserAuthenticationService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenService _tokenService;

    public UserAuthenticationService(IUserRepository userRepository, ITokenService tokenService)
    {
        _userRepository = userRepository;
        _tokenService = tokenService;
    }

    public async Task<Result<AuthenticationResponse>> AuthenticateAsync(AuthenticateCommand command)
    {
        var invalidEmail = !Email.TryParse(command.Login, out var _);
        var invalidUsername = string.IsNullOrWhiteSpace(command.Login); 
        if (invalidEmail && invalidUsername)
        {
            return new Result<AuthenticationResponse>(new ValidationException(new ValidationError()
            {
                PropertyName = "Login", ErrorMessage = "Você deve informar um usuário ou um e-mail válido"
            }));
        }
        
        var user = await _userRepository.GetAsync(command.Login!);
        var validPassword = user?.IsValidPassword(command.Password!) ?? false;

        if (user is null || !validPassword)
        {
            return new Result<AuthenticationResponse>(new ValidationException(new ValidationError()
                { PropertyName = "user", ErrorMessage = "Usuário ou senha inválidos" }));
        }

        var token = _tokenService.Generate(user);
        return token;
    }
}