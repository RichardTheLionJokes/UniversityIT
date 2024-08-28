using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Models.Auth;

namespace UniversityIT.Core.Abstractions.Auth.Users
{
    public interface IUsersRepository
    {
        Task Create(User user);
        Task<Guid> Update(Guid id, string userName, string passwordHash, string email, string fullName, string position, string phoneNumber);
        Task<User> GetByEmail(string email);
        Task<HashSet<Permission>> GetUserPermissions(Guid userId);
    }
}