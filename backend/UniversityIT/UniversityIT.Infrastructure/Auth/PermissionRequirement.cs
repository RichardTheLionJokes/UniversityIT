using Microsoft.AspNetCore.Authorization;
using UniversityIT.Core.Enums.Auth;

namespace UniversityIT.Infrastructure.Auth
{
    public class PermissionRequirement(Permission[] permissions) : IAuthorizationRequirement
    {
        public Permission[] Permissions { get; set; } = permissions;
    }
}