using Joiniv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joiniv.Infrastructure.Data.Configuration
{
    public class EventImageConfiguration : IEntityTypeConfiguration<EventImage>
    {
        public void Configure(EntityTypeBuilder<EventImage> builder)
        {
            builder.ToTable("event_images");
            builder.HasKey(ei => ei.Id);
            builder.Property(ei => ei.EventId)
                .IsRequired();
            builder.Property(ei => ei.ImageUrl)
                .IsRequired()
                .HasMaxLength(255);
            builder.Property(ei => ei.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasOne(ei => ei.Event)
                .WithMany(e => e.EventImages)
                .HasForeignKey(ei => ei.EventId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(ei => ei.EventId);
        }
    }
}