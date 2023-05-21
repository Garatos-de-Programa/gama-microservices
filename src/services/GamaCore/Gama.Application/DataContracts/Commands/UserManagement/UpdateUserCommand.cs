using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.DataContracts.Commands.UserManagement;

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