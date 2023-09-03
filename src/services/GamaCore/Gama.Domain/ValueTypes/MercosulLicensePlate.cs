using System.Text.RegularExpressions;

namespace Gama.Domain.ValueTypes
{
    public readonly struct MercosulLicensePlate
    {
        private const int NormalLicensePlateLength = 7;
        
        private const int MercosulLicensePlateLength = 8;

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

            if (value.Length > MercosulLicensePlateLength || value.Length < NormalLicensePlateLength)
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

            var letterSide = value[..3];
            var numberSide = value[4..];
            licensePlate = new MercosulLicensePlate(letterSide + numberSide);
            return true;
        }

        public static string Format(string? value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return string.Empty;
            }

            if (value.Length != NormalLicensePlateLength)
            {
                return value;
            }

            var letterSide = value[..3];
            var numberSide = value[3..];
            return letterSide + '-' + numberSide;
        }

        public override string ToString()
            => _value;

        public static implicit operator MercosulLicensePlate(string value)
            => new(value);

        public static implicit operator string(MercosulLicensePlate value) => value.ToString();
    }
}
