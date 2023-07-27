namespace Gama.Domain.Common;

public abstract class AuditableEntity : Entity
{
    public DateTime? CreatedAt { get; protected set; }

    public DateTime? UpdatedAt { get; protected set; }

    public DateTime? DeletedAt { get; protected set; }


    public virtual void Delete()
    {
        DeletedAt = DateTime.UtcNow;
    }
}