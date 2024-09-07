using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityIT.Core.Enums.Common;
using UniversityIT.DataAccess.Entities.Common;

namespace UniversityIT.DataAccess.Configurations.Common
{
    public class NetStatusConfiguration : IEntityTypeConfiguration<NetStatusEntity>
    {
        public void Configure(EntityTypeBuilder<NetStatusEntity> builder)
        {
            builder.HasKey(e => e.Id);

            var statuses = Enum
                .GetValues<NetStatus>()
                .Select(ss => new NetStatusEntity
                {
                    Id = (int)ss,
                    Name = ss.ToString()
                });

            builder.HasData(statuses);
        }
    }
}