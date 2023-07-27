namespace Gama.Application.UseCases.UserAgg.Responses;

public class AuthenticationResponse
{
    public string? Token { get; set; }
    public double ExpiresIn { get; set; }
    public string? TokenType => "Bearer";
}