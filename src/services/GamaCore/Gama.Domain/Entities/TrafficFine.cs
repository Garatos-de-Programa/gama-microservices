namespace Gama.Domain.Entities;

public class TrafficFine : AuditableEntity
{
    public int Id { get; set; }

    public string? LicensePlate { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public string? Photo { get; set; }

    public bool Active { get; set; }

    public bool Computed { get; set; }

    public ICollection<TrafficFineTrafficViolation> TrafficFineTrafficViolations { get; set; }
}