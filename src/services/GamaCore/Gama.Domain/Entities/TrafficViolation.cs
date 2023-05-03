namespace Gama.Domain.Entities;

public class TrafficViolation : AuditableEntity
{
    public short Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public bool Active { get; set; } = true;

    public void Update(TrafficViolation trafficViolation)
    {
        Code = trafficViolation.Code;
        Name = trafficViolation.Name;
        UpdatedAt = DateTime.UtcNow;
    }

    public override void Delete()
    {
        base.Delete();
        Active = false;
    }
}