using Flunt.Notifications;
using Gama.Application.Seedworks.ValidationContracts;

namespace Gama.Application.DataContracts.Commands.TrafficFineManagement;

public class CreateTrafficFineCommand : Notifiable<Notification>, IRequest
{
    public string? LicensePlate { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
    
    public IEnumerable<CreateTrafficFineTrafficViolationCommand>? TrafficViolations { get; set; }

    public void Validate()
    {
        AddNotifications(new CreateTrafficFineCommandContract(this));
    }
}