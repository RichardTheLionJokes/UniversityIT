using UniversityIT.Application.Abstractions.Auth;
using UniversityIT.Application.Abstractions.Common;
using UniversityIT.Core.Abstractions.Auth.Users;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.Core.Models.Auth;

namespace UniversityIT.Application.Services.Auth
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IPasswordGenerator _passwordGenerator;       
        private readonly IJwtProvider _jwtProvider;
        private readonly IMessageService _messageService;

        public UsersService(
            IUsersRepository usersRepository,
            IPasswordHasher passwordHasher,
            IPasswordGenerator passwordGenerator,
            IJwtProvider jwtProvider,
            IMessageService messageService)
        {
            _usersRepository = usersRepository;
            _passwordHasher = passwordHasher;
            _passwordGenerator = passwordGenerator;
            _jwtProvider = jwtProvider;
            _messageService = messageService;
        }

        public async Task Register(string userName, string email, string fullName, string position, string phoneNumber)
        {
            bool userExists = await _usersRepository.UserExists(email);

            if (!userExists)
            {
                var password = _passwordGenerator.Generate(9);
                var hashedPassword = _passwordHasher.Generate(password);

                var user = User.Create(Guid.NewGuid(), userName, hashedPassword, email, fullName, position, phoneNumber);

                await _usersRepository.Create(user);

                string message = $"You new password is {password}";
                string subject = $"Password for {email}";
                await _messageService.SendMessage(email, subject, message);
            }
        }

        public async Task<(string, User)> Login(string email, string password)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(password, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Failed to login");
            }

            var token = _jwtProvider.GenerateToken(user);

            return (token, user);
        }

        public async Task<User> GetUserById(Guid id)
        {
            return await _usersRepository.GetById(id);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _usersRepository.GetByEmail(email);
        }

        public async Task<Guid> UpdateUser(Guid id, string userName, string email, string fullName, string position, string phoneNumber)
        {
            return await _usersRepository.Update(id, userName, email, fullName, position, phoneNumber);
        }

        public async Task ChangePassword(string email, string oldPassword, string newPassword)
        {
            var user = await _usersRepository.GetByEmail(email);

            var result = _passwordHasher.Verify(oldPassword, user.PasswordHash);

            if (result == false)
            {
                throw new Exception("Incorrect old password");
            }

            var hashedPassword = _passwordHasher.Generate(newPassword);

            await _usersRepository.ChangePassword(email, hashedPassword);
        }

        public async Task ResetPassword(string email)
        {
            bool userExists = await _usersRepository.UserExists(email);

            if (userExists)
            {
                var password = _passwordGenerator.Generate(9);
                var hashedPassword = _passwordHasher.Generate(password);

                await _usersRepository.ChangePassword(email, hashedPassword);

                string message = $"You new password is {password}";
                string subject = $"Password for {email}";
                await _messageService.SendMessage(email, subject, message);
            }
        }
    }
}