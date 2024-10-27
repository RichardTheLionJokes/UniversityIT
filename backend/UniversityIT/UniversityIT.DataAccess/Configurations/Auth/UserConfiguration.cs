using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityIT.Core.Enums.Auth;
using UniversityIT.DataAccess.Entities.Auth;

namespace UniversityIT.DataAccess.Configurations.Auth
{
    public partial class UserConfiguration : IEntityTypeConfiguration<UserEntity>
    {
        public void Configure(EntityTypeBuilder<UserEntity> builder)
        {
            builder.HasKey(u => u.Id);

            builder.Property(u => u.UserName)
                .IsRequired();

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.Email)
                .IsRequired();

            builder.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<UserRoleEntity>(
                    r => r.HasOne<RoleEntity>().WithMany().HasForeignKey(e => e.RoleId),
                    u => u.HasOne<UserEntity>().WithMany().HasForeignKey(e => e.UserId));

            builder.HasMany(u => u.Tickets)
                .WithOne(t => t.User);
        }
    }
}