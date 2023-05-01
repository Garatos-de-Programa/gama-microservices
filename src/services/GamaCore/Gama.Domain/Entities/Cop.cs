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
    )
    {
        FirstName = firstName;
        LastName = lastName;
        Username = username;
        Email = email;
        Password = password;
        DocumentNumber = documentNumber;
        Encrypt();
    }
}