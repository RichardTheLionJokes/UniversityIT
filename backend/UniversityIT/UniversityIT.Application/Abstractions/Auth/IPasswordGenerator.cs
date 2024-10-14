namespace UniversityIT.Application.Abstractions.Auth
{
    public interface IPasswordGenerator
    {
        string Generate(int lenght);
    }
}