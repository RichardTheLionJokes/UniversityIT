using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityIT.Core.Models.ServMon;
using UniversityIT.Core.ValueObjects;
using UniversityIT.DataAccess.Entities.ServMon;

namespace UniversityIT.DataAccess.Configurations.ServMon
{
    public class ServerConfiguration : IEntityTypeConfiguration<ServerEntity>
    {
        public void Configure(EntityTypeBuilder<ServerEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.ComplexProperty(e => e.NetAddress, b =>
            {
                b.IsRequired();
                b.Property(g => g.NetName).HasColumnName("Name")
                    .HasMaxLength(NetAddress.MAX_NAME_LENGTH);
                b.Property(g => g.IpAddress).HasColumnName("IpAddress");
            });

            builder.Property(e => e.ShortDescription)
                .HasMaxLength(Server.MAX_SHORT_DESCR_LENGTH)
                .IsRequired();

            builder.Property(e => e.Description)
                .HasMaxLength(Server.MAX_DESCR_LENGTH);

            builder.Property(e => e.Activity)
                .IsRequired();

            builder.HasMany(s => s.ServEvents)
                .WithOne(se => se.Server);
        }
    }
}