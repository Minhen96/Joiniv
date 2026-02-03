namespace Joiniv.Domain.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid ParticipantId { get; set; }
        public Guid EventId { get; set; }
        public User Participant { get; set; } = null!;
        public Event Event { get; set; } = null!;
        public DateTimeOffset BookingDate { get; set; }
        public BookingStatus Status { get; set; } = BookingStatus.Pending;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }

    }

    public enum BookingStatus
    {
        Pending,
        Confirmed,
        Cancelled
    }
}
