using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;
using ValidationException = Gama.Domain.Exceptions.ValidationException;

namespace Gama.Application.UseCases.UserAgg.Commands;

public class AuthenticateCommand : Notifiable<Notification>, IRequest
{
    public string? Login { get; init; }

    public string? Password { get; init; }

    public void Validate()
    {
        AddNotifications(new AuthenticateCommandContract(this));
    }
}