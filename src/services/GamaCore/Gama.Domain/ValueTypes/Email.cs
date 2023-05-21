
using System.Text.RegularExpressions;

namespace Gama.Domain.ValueTypes;

public struct Email
{
    private static readonly Regex RegexValidator = new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$",
        RegexOptions.Compiled, TimeSpan.FromSeconds(2));
    
    private readonly string _value;

    public Email(string value)
    {
        _value = value;
    }

    public static Email Parse(string value)
    {
        if (TryParse(value, out var email))
        {
            return email;
        }

        throw new ArgumentException("E-mail inválido!");
    }
    
    public static bool TryParse(string? value, out Email email)
    {
        if (string.IsNullOrEmpty(value))
        {
            email = new Email();
            return false;
        }

        if (!RegexValidator.IsMatch(value))
        {
            email = new Email();
            return false;
        }
        
        email = new Email(value);
        return true;
    }

    public override string ToString()
        => _value;

    public static implicit operator Email(string value)
        => new Email(value);
}