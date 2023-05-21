using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Gama.Domain.ValueTypes
{
    public readonly struct MercosulLicensePlate
    {
        private static readonly Regex NormalRegexValidator = new(@"[a-zA-Z]{3}-[0-9]{4}",
        RegexOptions.Compiled, TimeSpan.FromSeconds(2));

        private static readonly Regex MercosulRegexValidator = new(@"[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}",
        RegexOptions.Compiled, TimeSpan.FromSeconds(2));

        private readonly string _value;

        public MercosulLicensePlate(string value)
        {
            _value = value;
        }

        public static MercosulLicensePlate Parse(string value)
        {
            if (TryParse(value, out var licensePlate))
            {
                return licensePlate;
            }

            throw new ArgumentException("Placa inválida!");
        }

        public static bool TryParse(string? value, out MercosulLicensePlate licensePlate)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                licensePlate = new MercosulLicensePlate();
                return false;
            }

            if (value.Length > 8 || value.Length < 7)
            {
                licensePlate = new MercosulLicensePlate();
                return false;
            }

            if (char.IsLetter(value, 4))
            {
                if (!MercosulRegexValidator.IsMatch(value))
                {
                    licensePlate = new MercosulLicensePlate();
                    return false;
                }

                licensePlate = new MercosulLicensePlate(value);
                return true;
            }


            if (!NormalRegexValidator.IsMatch(value))
            {
                licensePlate = new MercosulLicensePlate();
                return false;
            }

            licensePlate = new MercosulLicensePlate(value);
            return true;
        }

        public override string ToString()
            => _value;

        public static implicit operator MercosulLicensePlate(string value)
            => new(value);
    }
}
