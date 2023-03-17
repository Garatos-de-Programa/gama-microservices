namespace Gama.Application.DataContracts.Commands;

public class TokenCreationCommand
{
    public string Email { get; set; }

    public string Password { get; set; }

    public string Username { get; set; }

    public bool IsValid()
    {
        return HasEmailOrUsername() && !string.IsNullOrWhiteSpace(Password);
    }

    internal bool HasEmailOrUsername()
    {
        return !string.IsNullOrWhiteSpace(Email) || !string.IsNullOrWhiteSpace(Username);
    }
}