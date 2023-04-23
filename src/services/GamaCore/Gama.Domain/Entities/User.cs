using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities;

public class User : AuditableEntity
{
    public int Id { get; protected set; }
    
    public string FirstName { get; protected set; }

    public string LastName { get; protected set; }

    public string Username { get; protected set; }

    public string Email { get; protected set; }
    
    public string Password { get; protected set; }

    public string DocumentNumber { get; protected set; }

    public string Role { get; protected set; }

    public User(
        string firstName,
        string lastName,
        string username,
        string email,
        string password,
        string documentNumber,
        string role,
        DateTime? createdAt = null,
        DateTime? updatedAt = null,
        DateTime? deletedAt = null
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Email = email;
        Password = password;
        DocumentNumber = documentNumber;
        Role = role;
        CreatedAt = createdAt ?? DateTime.UtcNow;
        UpdatedAt = updatedAt;
        DeletedAt = deletedAt;
    }

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
}