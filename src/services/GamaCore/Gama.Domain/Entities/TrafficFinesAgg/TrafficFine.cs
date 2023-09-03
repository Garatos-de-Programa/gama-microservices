using Gama.Domain.Common;
using Gama.Domain.Entities.UsersAgg;
using Gama.Domain.Exceptions;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities.TrafficFinesAgg;

public class TrafficFine : AuditableEntity
{
    public int Id { get; set; }

    public string? LicensePlate { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool Active { get; set; }

    public bool Computed { get; set; }

    public int UserId { get; set; }

    public string? ImageUrl { get; set; }

    public User? User { get; set; }

    public ICollection<TrafficFineTrafficViolation>? TrafficFineTrafficViolations { get; set; }

    public Result<bool> Compute(User user)
    {
        if (user.IsDiferentUser(UserId))
        {
            return new Result<bool>(new ValidationException(new ValidationError()
            {
                PropertyName = "TrafficFine",
                ErrorMessage = "Operação invalida"
            }));
        }

        Computed = true;
        UpdatedAt = DateTime.UtcNow;

        return true;
    }

    public Result<bool> Delete(User user)
    {
        if (user.IsDiferentUser(UserId))
        {
            return new Result<bool>(new ValidationException(new ValidationError()
            {
                PropertyName = "TrafficFine",
                ErrorMessage = "Operação invalida"
            }));
        }

        if (Computed)
        {
            return new Result<bool>(new ValidationException(new ValidationError()
            {
                PropertyName = "TrafficFine",
                ErrorMessage = "Operação invalida, multa já computada"
            }));
        }

        Active = false;
        base.Delete();

        return true;
    }

    public bool IsUserAllowedToHandle(User user)
    {
        var isDiferentUser = user.IsDiferentUser(UserId);
        var isAdmin = user.IsRole(RolesName.Admin);

        if (isAdmin)
            return true;

        if (isDiferentUser)
            return false;

        return true;
    }

    public void PrepareToInsert(int userId)
    {
        CreatedAt = DateTime.UtcNow;
        Active = true;
        Computed = false;
        UserId = userId;
    }
}