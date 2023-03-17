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
        return _bCryptValue.Equals(password);
    }

    public static BCryptPassword Parse(string password) => new(password);

    public static implicit operator BCryptPassword(string password) => Parse(password);
}