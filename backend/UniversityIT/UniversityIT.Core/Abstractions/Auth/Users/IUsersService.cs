namespace UniversityIT.Core.Abstractions.Auth.Users
{
    public interface IUsersService
    {
        Task<string> Login(string email, string password);
        Task Register(string userName, string password, string email, string fullName, string position, string phone);
    }
}