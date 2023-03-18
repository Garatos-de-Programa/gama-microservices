using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Application.DataContracts.Commands;

public class TokenCreationCommand
{
    public string Email { get; init; }

    public string Password { get; init; }

    public string Username { get; init; }

    public Result<bool> IsValid()
    {
        var hasValidLogin = HasEmailOrUsername();
        var hasValidPassword = !string.IsNullOrWhiteSpace(Password);

        if (!hasValidLogin)
        {
            return new Result<bool>(new ValidationException(new ValidationError[]
                {
                    new ValidationError()
                    {
                        PropertyName = nameof(Username),
                        ErrorMessage = "Você deve informar um usuário ou um e-mail válido"
                    },
                    new ValidationError()
                    {
                        PropertyName = nameof(Email),
                        ErrorMessage = "Você deve informar um usuário ou um e-mail válido"
                    },
                }
            ));
        }

        if (!hasValidPassword)
        {
            return new Result<bool>(new ValidationException(new ValidationError()
            {
                PropertyName = nameof(Password),
                ErrorMessage = "Você deve informar uma senha"
            }));
        }

        return true;
    }

    internal bool HasEmailOrUsername()
    {
        return !string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(Username);
    }
}