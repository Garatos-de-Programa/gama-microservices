namespace Gama.Application.DataContracts.Commands.TrafficFineManagement;

public class UpdateTrafficViolationCommand
{
    public short Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
}