namespace Gama.Application.DataContracts.Responses.TrafficManagement;

public class GetTrafficViolationResponse
{
    public string Id { get; }

    public string Code { get; }

    public string Name { get; }
    
    public DateTime? CreatedAt { get; }

    public DateTime? UpdatedAt { get; }
    
    public DateTime? DeletedAt { get; }
}