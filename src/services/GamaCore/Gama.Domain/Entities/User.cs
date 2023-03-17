using Gama.Domain.Enums;
using Gama.Domain.ValueTypes;

namespace Gama.Domain.Entities;

public class User
{
    public string FirstName { get; protected set; }

    public string LastName { get; protected set; }

    public string Username { get; protected set; }

    public string Email { get; protected set; }
    public string Password { get; protected set; }

    public string DocumentNumber { get; protected set; }

    public Role Role { get; protected set; }

    public bool IsValidPassword(string password)
    {
        var encryptedPassword = BCryptPassword.Parse(password);

        return encryptedPassword.IsValid(Password);
    }
}