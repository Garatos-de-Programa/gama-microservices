namespace Gama.Domain.Entities;

public class TrafficViolation : AuditableEntity
{
    public string Id { get; set; }

    public string Code { get; set; }

    public string Name { get; set; }
}