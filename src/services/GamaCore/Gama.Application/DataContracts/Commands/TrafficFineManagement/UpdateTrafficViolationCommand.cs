using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.DataContracts.Commands.TrafficFineManagement;

public class UpdateTrafficViolationCommand : Notifiable<Notification>, IRequest
{
    public string? Code { get; set; }

    public string? Name { get; set; }

    public void Validate()
    {
        AddNotifications(new UpdateTrafficViolationCommandContract(this));
    }
}