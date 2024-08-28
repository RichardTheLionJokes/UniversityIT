using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniversityIT.Core.Abstractions.ServMon.Servers;
using UniversityIT.DataAccess.Configurations.Auth;
using UniversityIT.DataAccess.Entities.Auth;
using UniversityIT.DataAccess.Entities.ServMon;
using UniversityIT.DataAccess.Extentions;

namespace UniversityIT.DataAccess
{
    public class UniversityITDbContext(
        DbContextOptions<UniversityITDbContext> options,
        IOptions<AuthorizationOptions> authOptions) : DbContext(options)
    {
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<ServerEntity> Servers => Set<ServerEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityITDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));

            modelBuilder.Seed(authOptions.Value);
        }
    }
}
