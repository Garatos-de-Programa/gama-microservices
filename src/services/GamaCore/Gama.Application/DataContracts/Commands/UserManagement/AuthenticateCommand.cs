using Flunt.Notifications;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;
using ValidationException = Gama.Domain.Exceptions.ValidationException;

namespace Gama.Application.DataContracts.Commands.UserManagement;

public class AuthenticateCommand : Notifiable<Notification>
{
    public string? Login { get; init; }

    public string Password { get; init; }

    public Result<bool> IsValid()
    {
        var invalidLogin = string.IsNullOrWhiteSpace(Login);
        var invalidPassword = string.IsNullOrWhiteSpace(Password);

        if (invalidLogin)
        {
            return new Result<bool>(new ValidationException(new ValidationError[]
                {
                    new ValidationError()
                    {
                        PropertyName = nameof(Login),
                        ErrorMessage = "Você deve informar um usuário ou um e-mail válido"
                    }
                }
            ));
        }

        if (invalidPassword)
        {
            return new Result<bool>(new ValidationException(new ValidationError()
            {
                PropertyName = nameof(Password),
                ErrorMessage = "Você deve informar uma senha"
            }));
        }

        return true;
    }
}