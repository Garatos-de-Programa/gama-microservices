namespace Gama.Domain.ValueTypes
{
    public readonly struct Cpf
    {
        private readonly string _value;

        public Cpf(string value)
        {
            _value = value;
        }

        public static Cpf Parse(string value)
        {
            if (TryParse(value, out var email))
            {
                return email;
            }

            throw new ArgumentException("Cpf inválido!");
        }

        public static bool TryParse(string? value, out Cpf cpf)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                cpf = new Cpf();
                return false;
            }

            Span<int> cpfArray = stackalloc int[11];
            var count = 0;

            foreach (var @char in value)
            {
                if (char.IsDigit(@char))
                {
                    if (count > 10)
                    {
                        cpf = new Cpf();
                        return false;
                    }

                    cpfArray[count] = @char - '0';
                    count++;
                }
            }

            if (count != 11)
            {
                cpf = new Cpf();
                return false;
            }

            if (AreAllValuesEquals(ref cpfArray))
            {
                cpf = new Cpf();
                return false;
            }

            var totalDigits1 = 0;
            var totalDigits2 = 0;
            int mod1;
            int mod2;

            for (int i = 0; i < cpfArray.Length - 2; i++)
            {
                totalDigits1 += cpfArray[i] * (10 - i);
                totalDigits2 += cpfArray[i] * (11 - i);
            }

            mod1 = totalDigits1 % 11;
            if (mod1 < 2) { mod1 = 0; }
            else {  mod1 = 11 - mod1; }

            if (cpfArray[9] != mod1)
            {
                cpf = new Cpf();
                return false;
            }

            totalDigits2 += mod1 * 2;

            mod2 = totalDigits2 % 11;
            if (mod2 < 2) { mod2 = 0; } 
            else {  mod2 = 11 - mod2; }

            if (cpfArray[10] != mod2)
            {
                cpf = new Cpf();
                return false;
            }

            cpf = new Cpf(value);
            return true;
        }

        public override string ToString()
            => _value;

        public static implicit operator Cpf(string value)
            => new Cpf(value);

        static bool AreAllValuesEquals(ref Span<int> input)
        {
            for (int i = 1; i < 11; i++)
            {
                if (input[i] != input[0])
                {
                    return false;
                }
            }

            return true;
        }
    }
}
