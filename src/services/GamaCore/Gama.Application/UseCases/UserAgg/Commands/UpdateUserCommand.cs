using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.UseCases.UserAgg.Commands;

public class UpdateUserCommand : Notifiable<Notification>, IRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? DocumentNumber { get; set; }

    public void Validate()
    {
        AddNotifications(new UpdateUserCommandContract(this));
    }
}