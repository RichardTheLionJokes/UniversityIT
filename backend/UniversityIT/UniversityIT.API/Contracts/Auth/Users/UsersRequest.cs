using System.ComponentModel.DataAnnotations;

namespace UniversityIT.API.Contracts.Auth.Users
{
    public record UsersRequest(
        [Required] string UserName,
        [Required] string Email,
        string FullName = "",
        string Position = "",
        string PhoneNumber = "");
}