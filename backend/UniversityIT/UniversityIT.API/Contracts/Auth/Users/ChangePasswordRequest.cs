using System.ComponentModel.DataAnnotations;

namespace UniversityIT.API.Contracts.Auth.Users
{
    public record ChangePasswordRequest(
        [Required] string Email,
        [Required] string OldPassword,
        [Required] string NewPassword);
}