using Gama.Domain.Entities;

namespace Gama.Application.DataContracts.Commands.UserManagement;

public class CreateUserCommand
{
    public string FirstName { get; }

    public string LastName { get; }

    public string Username { get; }

    public string Email { get; }
    
    public string Password { get; }

    public string DocumentNumber { get; }

    public User ToUser()
    {
        return new Cop(
            FirstName,
            LastName,
            Username,
            Email,
            Password,
            DocumentNumber);
    }
}