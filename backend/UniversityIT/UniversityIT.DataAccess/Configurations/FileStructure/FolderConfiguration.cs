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

            builder.HasMany(flp => flp.Folders)
                .WithOne(flc => flc.Parent);

            builder.HasMany(fl => fl.Files)
                .WithOne(fl => fl.Parent);
        }
    }
}
