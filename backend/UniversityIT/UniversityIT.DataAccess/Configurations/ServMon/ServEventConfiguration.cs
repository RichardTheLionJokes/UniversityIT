using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityIT.DataAccess.Entities.ServMon;

namespace UniversityIT.DataAccess.Configurations.ServMon
{
    public class ServEventConfiguration : IEntityTypeConfiguration<ServEventEntity>
    {
        public void Configure(EntityTypeBuilder<ServEventEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.HappenedAt)
                .IsRequired();

            builder.Property(e => e.ServerId)
                .IsRequired();

            builder.HasOne(se => se.Server)
                .WithMany(s => s.ServEvents);
        }
    }
}