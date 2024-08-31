using UniversityIT.Core.Abstractions.Auth.Permissions;
using UniversityIT.Core.Abstractions.Auth.Users;
using UniversityIT.Core.Enums.Auth;

namespace UniversityIT.Application.Services.Auth
{
    public class PermissionsSevice : IPermissionsSevice
    {
        private readonly IUsersRepository _usersRepository;

        public PermissionsSevice(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public Task<HashSet<Permission>> GetPermissionsAsync(Guid userId)
        {
            return _usersRepository.GetUserPermissions(userId);
        }
    }
}