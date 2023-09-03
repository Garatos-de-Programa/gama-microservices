namespace Gama.Application.UseCases.TrafficFineAgg.Responses;

public class GetTrafficFineResponse
{
    public long Id { get; set; }

    public string LicensePlate { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool Active { get; set; }

    public bool Computed { get; set; }

    public IEnumerable<GetTrafficViolationResponse>? TrafficViolations { get; set; }

    public string? ImageUrl { get; set; }

    public bool Deleted => DeletedAt.HasValue;

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }
}