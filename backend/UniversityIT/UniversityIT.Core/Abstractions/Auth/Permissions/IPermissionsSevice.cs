using UniversityIT.Core.Enums.Auth;

namespace UniversityIT.Core.Abstractions.Auth.Permissions
{
    public interface IPermissionsSevice
    {
        Task<HashSet<Permission>> GetPermissionsAsync(Guid userId);
    }
}