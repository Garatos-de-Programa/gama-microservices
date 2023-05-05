namespace Gama.Application.DataContracts.Responses.UserManagement;

public class AuthenticationResponse
{
    public string Token { get; set; }
    public double ExpiresIn { get; set; }
    public string TokenType => "Bearer";
}