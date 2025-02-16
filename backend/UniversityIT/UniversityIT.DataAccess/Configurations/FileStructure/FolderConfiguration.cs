using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityIT.DataAccess.Entities.FileStructure;

namespace UniversityIT.DataAccess.Configurations.FileStructure
{
    public class FolderConfiguration : IEntityTypeConfiguration<FolderEntity>
    {
        public void Configure(EntityTypeBuilder<FolderEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.HasOne(e => e.Parent);

            builder.Property(e => e.ParentPath)
                .IsRequired();

            builder.HasMany(flp => flp.Folders)
                .WithOne(flc => flc.Parent)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(fl => fl.Files)
                .WithOne(fl => fl.Parent)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}