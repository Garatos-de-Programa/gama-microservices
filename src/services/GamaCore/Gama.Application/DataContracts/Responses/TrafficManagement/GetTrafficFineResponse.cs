namespace Gama.Application.DataContracts.Responses.TrafficManagement;

public class GetTrafficFineResponse
{
    public long Id { get; }

    public string LicensePlate { get; }

    public decimal Latitude { get; }

    public decimal Longitude { get; }

    public string Photo { get; }

    public bool Active { get; }

    public bool Computed { get; }
    
    public IEnumerable<GetTrafficViolationResponse> TrafficViolations { get; }
    
    public DateTime? CreatedAt { get; }

    public DateTime? UpdatedAt { get; }
    
    public DateTime? DeletedAt { get; }
}