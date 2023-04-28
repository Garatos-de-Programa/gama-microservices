namespace Gama.Application.DataContracts.Commands.TrafficFineManagement;

public class UpdateTrafficFineCommand
{
    public long Id { get; set; }
    
    public string Photo { get; set; }

    public bool Active { get; set; }

    public bool Computed { get; set; }
    
    public IEnumerable<CreateTrafficFineTrafficViolationCommand> TrafficViolations { get; set; }
}