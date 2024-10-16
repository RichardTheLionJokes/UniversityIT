using CSharpFunctionalExtensions;

namespace UniversityIT.Core.Models.Auth
{
    public class User
    {
        private User(Guid id, string userName, string passwordHash, string email, string fullName, string position, string phoneNumber)
        {
            Id = id;
            UserName = userName;
            PasswordHash = passwordHash;
            Email = email;
            FullName = fullName;
            Position = position;
            PhoneNumber = phoneNumber;
        }
        public Guid Id { get; set; }
        public string UserName { get; private set; }
        public string PasswordHash { get; private set; }
        public string Email { get; private set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }

        public static Result<User> Create(Guid id, string userName, string passwordHash, string email, string fullName, string position, string phoneNumber)
        {
            var user = new User(id, userName, passwordHash, email, fullName, position, phoneNumber);

            return Result.Success(user);
        }
    }
}
