using UniversityIT.Core.Models.Auth;

namespace UniversityIT.Core.Abstractions.Auth.Users
{
    public interface IUsersService
    {
        Task<(string, User)> Login(string email, string password);
        Task Register(string userName, string password, string email, string fullName, string position, string phone);
    }
}