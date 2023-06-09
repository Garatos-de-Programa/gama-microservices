﻿using Gama.Domain.Common;
using Gama.Domain.Constants;
using Gama.Domain.Exceptions;
using Gama.Domain.Models.Users;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Models.TrafficFines;

public class TrafficFine : AuditableEntity
{
    public int Id { get; set; }

    public string? LicensePlate { get; set; }

    public decimal Latitude { get; set; }

    public decimal Longitude { get; set; }

    public bool Active { get; set; }

    public bool Computed { get; set; }

    public int UserId { get; set; }

    public User User { get; set; }

    public ICollection<TrafficFineTrafficViolation> TrafficFineTrafficViolations { get; set; }

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

    public void PrepareToInsert()
    {
        CreatedAt = DateTime.UtcNow;
        Active = true;
        Computed = false;
    }
}