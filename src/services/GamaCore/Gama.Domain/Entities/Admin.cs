using Gama.Domain.Constants;

namespace Gama.Domain.Entities;

public class Admin : User
{
    public Admin(
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
        Roles.Admin
        )
    {
        Encrypt();
    }
}