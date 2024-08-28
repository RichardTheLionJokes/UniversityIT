using System.ComponentModel.DataAnnotations;

namespace UniversityIT.API.Contracts.Auth.Users
{
    public record LoginUserRequest(
        [Required] string Email,
        [Required] string Password);
}
