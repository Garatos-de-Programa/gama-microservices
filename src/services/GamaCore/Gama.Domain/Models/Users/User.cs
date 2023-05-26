using Gama.Domain.Common;
using Gama.Domain.Constants;
using Gama.Domain.Models.TrafficFines;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Models.Users;

public class User : AuditableEntity
{
    public int Id { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Username { get; set; }

    public string? Email { get; set; }

    public string? Password { get; set; }

    public string? DocumentNumber { get; set; }

    public bool Active { get; set; }

    public ICollection<UserRoles> Roles { get; set; }

    public IEnumerable<TrafficFine>? TrafficFines { get; set; }

    public bool IsValidPassword(string password)
    {
        return BCryptPassword.IsValid(password, Password);
    }

    public void Update(string? firstName, string? lastName, string? documentNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        UpdatedAt = DateTime.UtcNow;
        DocumentNumber = Cpf.GetDigits(documentNumber);
    }

    public void ChangePassword(string newPassword)
    {
        Password = newPassword;
        Encrypt();
        UpdatedAt = DateTime.UtcNow;
    }

    public override void Delete()
    {
        Active = false;
        base.Delete();
    }

    public void AddRole(string role)
    {
        Roles ??= new List<UserRoles>();

        var roleToInsert = RolesName.Roles[role];

        if (Roles.Any(x => x.Role.Id == roleToInsert.Id))
            return;

        Roles.Add(new() { Role = roleToInsert });
    }

    public void PrepareToInsert()
    {
        Encrypt();
        Active = true;
        CreatedAt = DateTime.UtcNow;
        DocumentNumber = Cpf.GetDigits(DocumentNumber);
    }

    public bool IsDiferentUser(int userId)
    {
        return userId != Id;
    }

    public bool IsRole(string role)
    {
        return Roles?.Any(r => r.Role.Name == role) ?? false;
    }

    internal void Encrypt()
    {
        Password = BCryptPassword.Parse(Password);
    }

    internal bool IsDiferentUser(User user)
    {
        return user.Id != Id;
    }
}