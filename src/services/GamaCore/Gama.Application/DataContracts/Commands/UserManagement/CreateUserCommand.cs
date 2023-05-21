using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.DataContracts.Commands.UserManagement;

public class CreateUserCommand : Notifiable<Notification>, IRequest
{
    public string? FirstName { get; set; }

    public string? LastName { get; set;}

    public string? Username { get; set;}

    public string? Email { get; set;}
    
    public string? Password { get; set;}

    public string? DocumentNumber { get; set;}

    public void Validate()
    {
        AddNotifications(new CreateUserCommandContract(this));
    }
}