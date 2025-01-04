using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using UniversityIT.DataAccess.Entities.FileStructure;

namespace UniversityIT.DataAccess.Configurations.FileStructure
{
    public class FileConfiguration : IEntityTypeConfiguration<FileEntity>
    {
        public void Configure(EntityTypeBuilder<FileEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
               .IsRequired();

            builder.Property(e => e.StorageType)
               .IsRequired();

            builder.Property(e => e.FileRefValue)
               .IsRequired();

            builder.Property(e => e.ParentId)
                .IsRequired();

            builder.HasOne(fl => fl.Parent)
                .WithMany(fr => fr.Files);
        }
    }
}