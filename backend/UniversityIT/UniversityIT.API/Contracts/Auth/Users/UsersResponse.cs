namespace UniversityIT.API.Contracts.Auth.Users
{
    public record UsersResponse(
        Guid id,
        string Name,
        string Email,
        string FullName,
        string Position,
        string PhoneNumber);
}
