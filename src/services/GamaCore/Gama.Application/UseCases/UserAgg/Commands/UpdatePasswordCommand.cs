using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.UseCases.UserAgg.Commands;

public class UpdatePasswordCommand : Notifiable<Notification>, IRequest
{
    public string? Login { get; set; }

    public string? NewPassword { get; set; }

    public string? OldPassword { get; set; }

    public void Validate()
    {
        AddNotifications(new UpdatePasswordCommandContract(this));
    }
}