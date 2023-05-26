using Gama.Domain.Common;

namespace Gama.Domain.Models.TrafficFines;

public class TrafficViolation : AuditableEntity
{
    public int Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }

    public bool Active { get; set; } = true;

    public ICollection<TrafficFineTrafficViolation> TrafficFineTrafficViolations { get; set; }

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

    public void PrepareToInsert()
    {
        CreatedAt = DateTime.UtcNow;
    }
}