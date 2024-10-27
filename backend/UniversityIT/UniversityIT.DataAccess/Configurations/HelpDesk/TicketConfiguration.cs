using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UniversityIT.DataAccess.Entities.HelpDesk;

namespace UniversityIT.DataAccess.Configurations.HelpDesk
{
    public class TicketConfiguration : IEntityTypeConfiguration<TicketEntity>
    {
        public void Configure(EntityTypeBuilder<TicketEntity> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .IsRequired();

            builder.Property(e => e.NotificationsSent)
                .IsRequired();

            builder.Property(e => e.IsCompleted)
                .IsRequired();

            builder.Property(e => e.UserId)
                .IsRequired();

            builder.HasOne(t => t.User)
                .WithMany(u => u.Tickets);
        }
    }
}
