namespace UniversityIT.Application.Abstractions.Auth
{
    public interface IPasswordGenerator
    {
        string Generate(int length = 0);
        bool PasswordIsCorrect(string password);
    }
}