using System.ComponentModel.DataAnnotations;

namespace UniversityIT.API.Contracts.Auth.Users
{
    public record LoginUsersRequest(
        [Required] string Email,
        [Required] string Password);
}