using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityIT.Core.Enums.ServMon;
using UniversityIT.DataAccess.Entities.ServMon;

namespace UniversityIT.DataAccess.Configurations.ServMon
{
    public class ServStatusConfiguration : IEntityTypeConfiguration<ServStatusEntity>
    {
        public void Configure(EntityTypeBuilder<ServStatusEntity> builder)
        {
            builder.HasKey(e => e.Id);

            var statuses = Enum
                .GetValues<ServStatus>()
                .Select(ss => new ServStatusEntity
                {
                    Id = (int)ss,
                    Name = ss.ToString()
                });

            builder.HasData(statuses);
        }
    }
}