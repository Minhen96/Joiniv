namespace Joiniv.Domain.Entities
{
    public class Event
    {
        public Guid Id { get; set; }
        public required string Title { get; set; }
        public required string Description { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public required string Location { get; set; }
        public int CurrentParticipants { get; set; } = 0;
        public int MaxParticipants { get; set; }
        public Guid OrganizerId { get; set; }
        public User Organizer { get; set; } = null!;
        public List<EventImage> EventImages { get; set; } = [];
        public List<Booking> Bookings { get; set; } = [];
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
