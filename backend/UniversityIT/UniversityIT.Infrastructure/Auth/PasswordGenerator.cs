using UniversityIT.Application.Abstractions.Auth;

namespace UniversityIT.Infrastructure.Auth
{
    public class PasswordGenerator : IPasswordGenerator
    {
        readonly string numbers = "0123456789";
        readonly string lowerLetters = "abcdefghijkmnopqrstuvwxyz";
        readonly string upperLetters = "ABCDEFGHJKLMNOPQRSTUVWXYZ";
        
        public string Generate(int lenght)
        {
            if (lenght < 6) throw new Exception();

            int x = lenght / 3;
            int y = lenght % 3;

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
    }
}
