using Microsoft.Extensions.Options;
using UniversityIT.Application.Abstractions.Auth;

namespace UniversityIT.Infrastructure.Auth
{
    public class PasswordGenerator : IPasswordGenerator
    {
        private const string numbers = "0123456789";
        private const string lowerLetters = "abcdefghijkmnopqrstuvwxyz";
        private const string upperLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ";

        private readonly PasswordPolicyOptions _passwordPolicyOptions;

        public PasswordGenerator(IOptions<PasswordPolicyOptions> options)
        {
            _passwordPolicyOptions = options.Value;
        }

        public string Generate(int length)
        {
            length = length == 0 ? _passwordPolicyOptions.DefaultLength : length;
            if (length < _passwordPolicyOptions.MinLength || length > _passwordPolicyOptions.MaxLength) throw new Exception();

            int x = length / 3;
            int y = length % 3;

            string password = "";

            Random rnd = new Random();

            for (int i = 0; i < x; i++)
            {
                int j = rnd.Next(numbers.Length);
                password += numbers[j];
                j = rnd.Next(lowerLetters.Length);
                password += lowerLetters[j];
                j = rnd.Next(upperLetters.Length);
                password += upperLetters[j];
            }

            if (y > 0)
            {
                int j = rnd.Next(lowerLetters.Length);
                password += lowerLetters[j];
            }

            return string.Join("", password.OrderBy(x => rnd.Next()));
        }

        public bool PasswordIsCorrect(string password)
        {
            bool isCorrect = true;
            if (password.Length < _passwordPolicyOptions.MinLength || password.Length > _passwordPolicyOptions.MaxLength)
            {
                isCorrect = false;
            }

            return isCorrect;
        }
    }
}
