namespace Gama.Domain.Entities;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; protected set; }

    public DateTime? UpdatedAt { get; protected set; }
    public DateTime? DeletedAt { get; private set; }

    public void Delete()
    {
        DeletedAt = DateTime.UtcNow;
    }
}