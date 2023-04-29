using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities;

public class User : AuditableEntity
{
    public int Id { get; protected set; }
    
    public string? FirstName { get; protected set; }

    public string? LastName { get; protected set; }

    public string? Username { get; protected set; }

    public string? Email { get; protected set; }
    
    public string? Password { get; protected set; }

    public string? DocumentNumber { get; protected set; }

    public bool Active { get; protected set; }

    public UserAddress? Address { get; set; }

    public bool IsValidPassword(string password)
    {
        return BCryptPassword.IsValid(password, Password);
    }

    internal void Encrypt()
    {
        Password= BCryptPassword.Parse(Password);
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
}