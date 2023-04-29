namespace Gama.Domain.Entities;

public class TrafficViolation : AuditableEntity
{
    public short Id { get; set; }

    public string? Code { get; set; }

    public string? Name { get; set; }
}