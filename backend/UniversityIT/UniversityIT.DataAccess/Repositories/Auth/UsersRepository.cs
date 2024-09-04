using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Abstractions.Auth.Users;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.Core.Models.Auth;
using UniversityIT.DataAccess.Entities.Auth;

namespace UniversityIT.DataAccess.Repositories.Auth
{
    public class UsersRepository : IUsersRepository
    {
        private readonly UniversityITDbContext _context;
        //private readonly IMapper _mapper;

        public UsersRepository(UniversityITDbContext context)
        {
            _context = context;
            //_mapper = mapper;
        }

        public async Task Create(User user)
        {
            var roleEntity = await _context.Roles
                .SingleOrDefaultAsync(r => r.Id == (int)Role.Employee)
                ?? throw new InvalidOperationException();

            var userEntity = new UserEntity
            {
                Id = user.Id,
                UserName = user.UserName,
                PasswordHash = user.PasswordHash,
                Email = user.Email,
                FullName = user.FullName,
                Position = user.Position,
                PhoneNumber = user.PhoneNumber,
                Roles = [roleEntity]
            };

            await _context.Users.AddAsync(userEntity);
            await _context.SaveChangesAsync();
        }

        public async Task<Guid> Update(Guid id, string userName, string passwordHash, string email, string fullName, string position, string phoneNumber)
        {
            await _context.Users
                .Where(u => u.Id == id)
                .ExecuteUpdateAsync(spc => spc
                    .SetProperty(u => u.UserName, u => userName)
                    .SetProperty(u => u.PasswordHash, u => passwordHash)
                    .SetProperty(u => u.Email, u => email)
                    .SetProperty(u => u.FullName, u => fullName)
                    .SetProperty(u => u.Position, u => position)
                    .SetProperty(u => u.PhoneNumber, u => phoneNumber));

            return id;
        }

        public async Task<User> GetByEmail(string email)
        {
            var userEntity = await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception();

            //return _mapper.Map<User>(userEntity);
            return User.Create(userEntity.Id, userEntity.UserName, userEntity.PasswordHash, userEntity.Email, userEntity.FullName, userEntity.Position, userEntity.PhoneNumber);
        }

        public async Task<HashSet<Permission>> GetUserPermissions(Guid userId)
        {
            var roles = await _context.Users
                .AsNoTracking()
                .Include(u => u.Roles)
                .ThenInclude(r => r.Permissions)
                .Where(u => u.Id == userId)
                .Select(u => u.Roles)
                .ToListAsync();

            return roles
                .SelectMany(r => r)
                .SelectMany(r => r.Permissions)
                .Select(p => (Permission)p.Id)
                .ToHashSet();
        }
    }
}