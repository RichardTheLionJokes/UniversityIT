using Microsoft.EntityFrameworkCore;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.DataAccess.Entities.Auth;
using UniversityIT.DataAccess.Entities.FileStructure;

namespace UniversityIT.DataAccess.Extentions
{
    public static class ModelBuilderExtentions
    {
        public static void Seed(
            this ModelBuilder modelBuilder,
            AuthorizationOptions authOptions)
        {
            string passwordHash = BCrypt.Net.BCrypt.EnhancedHashPassword(authOptions.AdminUserSettings.Password);
            UserEntity adminUser = new UserEntity
            {
                Id = Guid.NewGuid(),
                UserName = authOptions.AdminUserSettings.UserName,
                PasswordHash = passwordHash,
                Email = authOptions.AdminUserSettings.Email,
                FullName = "",
                Position = "",
                PhoneNumber = ""
            };

            modelBuilder.Entity<UserEntity>().HasData(adminUser);

            modelBuilder.Entity<UserRoleEntity>().HasData(new UserRoleEntity
            {
                UserId = adminUser.Id,
                RoleId = (int)Role.Admin
            });

            modelBuilder.Entity<FolderEntity>().HasData(new FolderEntity
            {
                Id = 1,
                Name = "Documents"
            });
        }
    }
}