namespace Gama.Domain.Entities;

public class TrafficFine : AuditableEntity
{
    public int Id { get; set; }

    public string? LicensePlate { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool Active { get; set; }

    public bool Computed { get; set; }

    public ICollection<TrafficFineTrafficViolation> TrafficFineTrafficViolations { get; set; }

    public void Compute()
    {
        Computed = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public override void Delete()
    {
        Active = false;
        base.Delete();
    }
}