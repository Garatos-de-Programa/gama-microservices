namespace Gama.Domain.ValueTypes;

using BCrypt.Net;

public struct BCryptPassword
{
    private string _bCryptValue;
    private BCryptPassword(string password)
    {
        _bCryptValue = BCrypt.HashPassword(password);
    }

    public bool IsValid(string password)
    {
        return BCrypt.Verify(password, _bCryptValue);
    }

    public static bool IsValid(string password, string encryptedPassword)
    {
        return BCrypt.Verify(password, encryptedPassword);
    }

    public static BCryptPassword Parse(string password) => new(password);

    public static implicit operator BCryptPassword(string password) => Parse(password);

    public static implicit operator string(BCryptPassword encryptedPassword) => encryptedPassword._bCryptValue;
}