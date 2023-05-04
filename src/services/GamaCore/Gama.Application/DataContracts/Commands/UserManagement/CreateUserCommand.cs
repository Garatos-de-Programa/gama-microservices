namespace Gama.Application.DataContracts.Commands.UserManagement;

public class CreateUserCommand
{
    public string FirstName { get; set; }

    public string LastName { get; set;}

    public string Username { get; set;}

    public string Email { get; set;}
    
    public string Password { get; set;}

    public string DocumentNumber { get; set;}
}