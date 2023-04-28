namespace Gama.Application.DataContracts.Commands.TrafficFineManagement;

public class CreateTrafficFineCommand
{
    public string LicensePlate { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }
    
    public IEnumerable<CreateTrafficFineTrafficViolationCommand> TrafficViolations { get; set; }
}