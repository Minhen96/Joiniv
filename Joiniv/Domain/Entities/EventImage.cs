namespace Joiniv.Domain.Entities
{
    public class EventImage
    {
        public Guid Id { get; set; }
        public required string ImageUrl { get; set; }
        public Guid EventId { get; set; }
        public Event Event { get; set; } = null!;
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset UpdatedAt { get; set; }
    }
}
