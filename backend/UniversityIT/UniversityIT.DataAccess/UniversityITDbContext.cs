using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using UniversityIT.DataAccess.Configurations.Auth;
using UniversityIT.DataAccess.Entities.Auth;
using UniversityIT.DataAccess.Entities.FileStructure;
using UniversityIT.DataAccess.Entities.HelpDesk;
using UniversityIT.DataAccess.Entities.ServMon;
using UniversityIT.DataAccess.Extentions;

namespace UniversityIT.DataAccess
{
    public class UniversityITDbContext(
        DbContextOptions<UniversityITDbContext> options,
        IOptions<AuthorizationOptions> authOptions) : DbContext(options)
    {
        // Auth
        public DbSet<RoleEntity> Roles => Set<RoleEntity>();
        public DbSet<UserEntity> Users => Set<UserEntity>();

        // ServMon
        public DbSet<ServerEntity> Servers => Set<ServerEntity>();
        public DbSet<ServEventEntity> ServEvents => Set<ServEventEntity>();

        // HelpDesk
        public DbSet<TicketEntity> Tickets => Set<TicketEntity>();

        // FileStructure
        public DbSet<FolderEntity> Folders => Set<FolderEntity>();
        public DbSet<FileEntity> Files => Set<FileEntity>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UniversityITDbContext).Assembly);

            modelBuilder.ApplyConfiguration(new RolePermissionConfiguration(authOptions.Value));

            modelBuilder.Seed(authOptions.Value);

            base.OnModelCreating(modelBuilder);
        }
    }
}