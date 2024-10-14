using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Models.Auth;

namespace UniversityIT.Core.Abstractions.Auth.Users
{
    public interface IUsersRepository
    {
        Task Create(User user);
        Task<User> GetById(Guid id);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permission>> GetUserPermissions(Guid userId);
        Task<Guid> Update(Guid id, string userName, string email, string fullName, string position, string phoneNumber);
        Task ChangePassword(string email, string passwordHash);
        Task<bool> UserExists(string email);
    }
}