using System.Text;

namespace Gama.Application.Options;

public class JwtOptions
{
    public string Issuer { get; init; }
    
    public string Audience { get; init; }
    
    public string SecretKey { get; init; }

    public byte[] GetSecretKeyBytes()
    {
        return Encoding.ASCII.GetBytes(SecretKey);
    }
}