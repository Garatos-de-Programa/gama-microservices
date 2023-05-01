using Gama.Domain.Constants;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities;

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

    public UserAddress? Address { get; set; }

    public IList<UserRoles>? UserRoles { get; set; }

    public bool IsValidPassword(string password)
    {
        return BCryptPassword.IsValid(password, Password);
    }

    public void Update(string firstName, string lastName, string documentNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        DocumentNumber = documentNumber;
        UpdatedAt = DateTime.UtcNow;
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
        UserRoles ??= new List<UserRoles>();

        var roleToInsert = RolesName.Roles[role];

        if (UserRoles.Any(x => x.RoleId == roleToInsert.Id))
            return;

        UserRoles.Add(new UserRoles()
        {
            RoleId = roleToInsert.Id,
            Role = roleToInsert
        });
    }

    public void PrepareToInsert()
    {
        Encrypt();
        Active = true;
        CreatedAt = DateTime.UtcNow;
    }

    internal void Encrypt()
    {
        Password = BCryptPassword.Parse(Password);
    }
}