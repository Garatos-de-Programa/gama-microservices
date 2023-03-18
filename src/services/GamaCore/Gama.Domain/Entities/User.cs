using Gama.Domain.Enums;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities;

public class User
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
        string documentNumber
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Email = email;
        Password = BCryptPassword.Parse(password);
        DocumentNumber = documentNumber;
    }

    public bool IsValidPassword(string password)
    {
        return BCryptPassword.IsValid(password, Password);
    }
}