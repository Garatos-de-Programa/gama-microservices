namespace Gama.Domain.Entities;

public abstract class AuditableEntity
{
    public DateTime? CreatedAt { get; protected set; }

    public DateTime? UpdatedAt { get; protected set; }
    
    public DateTime? DeletedAt { get; protected set; }

    public string? ModifiedBy { get; protected set; }

    public virtual void Delete()
    {
        DeletedAt = DateTime.UtcNow;
    }
}