using Gama.Domain.Enums;

namespace Gama.Domain.Entities;

public class Cop : User
{
    public Cop(
        string firstName,
        string lastName,
        string username,
        string email,
        string password,
        string documentNumber
    ) : base(
        firstName,
        lastName,
        username,
        email,
        password,
        documentNumber,
        Roles.Cop.ToString()
    )
    {
        Encrypt();
    }
}