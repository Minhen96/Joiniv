using Joiniv.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Joiniv.Infrastructure.Data.Configuration
{
    public class BookingConfiguration : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.ToTable("bookings");
            builder.HasKey(b => b.Id);
            builder.Property(b => b.ParticipantId)
                .IsRequired();
            builder.Property(b => b.EventId)
                .IsRequired();
            builder.Property(b => b.BookingDate)
                .IsRequired();
            builder.Property(b => b.Status)
                .IsRequired()
                .HasConversion<string>(); // Convert enum to string for storage
            builder.Property(b => b.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.Property(b => b.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("CURRENT_TIMESTAMP");
            builder.HasOne(b => b.Event)
                .WithMany(e => e.Bookings)
                .HasForeignKey(b => b.EventId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.Participant)
                .WithMany() // Or u => u.Bookings if we add that list to User.cs
                .HasForeignKey(b => b.ParticipantId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasIndex(b => new { b.ParticipantId, b.EventId })
                .IsUnique();
        }
    }
}