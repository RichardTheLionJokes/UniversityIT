using UniversityIT.Core.Models.Auth;

namespace UniversityIT.Core.Abstractions.Auth.Users
{
    public interface IUsersService
    {
        Task<(string, User)> Login(string email, string password);
        Task Register(string userName, string email, string fullName, string position, string phone);
        Task<User> GetUserById(Guid id);
        Task<User> GetUserByEmail(string email);
        Task<Guid> UpdateUser(Guid id, string userName, string email, string fullName, string position, string phoneNumber);
        Task ChangePassword(string email, string oldPassword, string newPassword);
        Task ResetPassword(string email);
    }
}