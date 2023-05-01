namespace Gama.Domain.Entities;

public class TrafficViolation : AuditableEntity
{
    public short Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public void Update(TrafficViolation trafficViolation)
    {
        Code = trafficViolation.Code;
        Name = trafficViolation.Name;
        UpdatedAt = DateTime.UtcNow;
    }
}